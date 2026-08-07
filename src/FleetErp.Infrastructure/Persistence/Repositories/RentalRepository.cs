using FleetErp.Application.Common;
using FleetErp.Application.Rentals.Interfaces;
using FleetErp.Domain.Entities.Rentals;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly FleetErpDbContext _context;

    public RentalRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Rental?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Rentals
            .Include(r => r.Vehicle)
            .Include(r => r.Customer)
            .Include(r => r.Status)
            .Include(r => r.PaymentStatus)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<Rental?> GetByIdWithPaymentsAsync(int id, CancellationToken ct = default)
    {
        return await _context.Rentals
            .Include(r => r.Vehicle)
            .Include(r => r.Customer)
            .Include(r => r.Status)
            .Include(r => r.PaymentStatus)
            .Include(r => r.Payments)
                .ThenInclude(p => p.PaymentMethod)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<PagedResult<Rental>> GetPagedAsync(int page, int pageSize, string? search, int? customerId, int? vehicleId, int? statusId, CancellationToken ct = default)
    {
        var query = _context.Rentals
            .Include(r => r.Vehicle)
            .Include(r => r.Customer)
            .Include(r => r.Status)
            .Include(r => r.PaymentStatus)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(r =>
                r.RentalNumber.Contains(search) ||
                r.Vehicle.PlateNumber.Contains(search) ||
                r.Customer.FullName.Contains(search));
        }

        if (customerId.HasValue)
        {
            query = query.Where(r => r.CustomerId == customerId.Value);
        }

        if (vehicleId.HasValue)
        {
            query = query.Where(r => r.VehicleId == vehicleId.Value);
        }

        if (statusId.HasValue)
        {
            query = query.Where(r => r.StatusId == statusId.Value);
        }

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(r => r.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<Rental>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<bool> RentalNumberExistsAsync(string rentalNumber, CancellationToken ct = default)
    {
        return await _context.Rentals.AnyAsync(r => r.RentalNumber == rentalNumber, ct);
    }

    public async Task<bool> VehicleHasActiveRentalAsync(int vehicleId, int? excludeRentalId = null, CancellationToken ct = default)
    {
        // Active rental statuses (Reserved, Active) - check by status codes
        var query = _context.Rentals
            .Include(r => r.Status)
            .Where(r => r.VehicleId == vehicleId &&
                       (r.Status.Code == "RESERVED" || r.Status.Code == "ACTIVE"));

        if (excludeRentalId.HasValue)
        {
            query = query.Where(r => r.Id != excludeRentalId.Value);
        }

        return await query.AnyAsync(ct);
    }

    public async Task<int> GetNextRentalSequenceAsync(DateTime date, CancellationToken ct = default)
    {
        var datePrefix = date.ToString("yyyyMMdd");
        var pattern = $"RNT-{datePrefix}-%";

        var maxNumber = await _context.Rentals
            .IgnoreQueryFilters() // Include deleted to ensure uniqueness
            .Where(r => EF.Functions.Like(r.RentalNumber, pattern))
            .Select(r => r.RentalNumber)
            .OrderByDescending(n => n)
            .FirstOrDefaultAsync(ct);

        if (maxNumber == null)
        {
            return 1;
        }

        // Extract sequence from RNT-YYYYMMDD-XXXX
        var sequencePart = maxNumber.Split('-').LastOrDefault();
        if (int.TryParse(sequencePart, out var sequence))
        {
            return sequence + 1;
        }

        return 1;
    }

    public async Task<IReadOnlyList<Rental>> GetActiveAsync(CancellationToken ct = default)
    {
        return await _context.Rentals
            .Include(r => r.Vehicle)
            .Include(r => r.Customer)
            .Include(r => r.Status)
            .Include(r => r.PaymentStatus)
            .Where(r => r.Status.Code == "ACTIVE" || r.Status.Code == "RESERVED")
            .OrderBy(r => r.StartDate)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Rental rental, CancellationToken ct = default)
    {
        await _context.Rentals.AddAsync(rental, ct);
    }

    public void Update(Rental rental)
    {
        _context.Rentals.Update(rental);
    }

    public void Delete(Rental rental)
    {
        rental.IsDeleted = true;
        _context.Rentals.Update(rental);
    }
}
