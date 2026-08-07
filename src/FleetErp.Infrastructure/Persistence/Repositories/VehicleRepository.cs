using FleetErp.Application.Common;
using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly FleetErpDbContext _context;

    public VehicleRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Include(v => v.Investor)
            .Include(v => v.Status)
            .Include(v => v.VehicleType)
            .Include(v => v.FuelType)
            .Include(v => v.TransmissionType)
            .FirstOrDefaultAsync(v => v.Id == id, ct);
    }

    public async Task<Vehicle?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Include(v => v.Investor)
            .Include(v => v.Status)
            .Include(v => v.VehicleType)
            .Include(v => v.FuelType)
            .Include(v => v.TransmissionType)
            .Include(v => v.Documents)
                .ThenInclude(d => d.DocumentType)
            .FirstOrDefaultAsync(v => v.Id == id, ct);
    }

    public async Task<PagedResult<Vehicle>> GetPagedAsync(
        int page,
        int pageSize,
        string? search = null,
        int? investorId = null,
        int? statusId = null,
        CancellationToken ct = default)
    {
        var query = _context.Vehicles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(v =>
                v.PlateNumber.Contains(search) ||
                v.Make.Contains(search) ||
                v.Model.Contains(search) ||
                (v.Vin != null && v.Vin.Contains(search)) ||
                v.Investor.FullName.Contains(search));
        }

        if (investorId.HasValue)
        {
            query = query.Where(v => v.InvestorId == investorId.Value);
        }

        if (statusId.HasValue)
        {
            query = query.Where(v => v.StatusId == statusId.Value);
        }

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Include(v => v.Investor)
            .Include(v => v.Status)
            .Include(v => v.VehicleType)
            .Include(v => v.Documents)
            .OrderBy(v => v.PlateNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<Vehicle>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<bool> PlateNumberExistsAsync(string plateNumber, int? excludeVehicleId = null, CancellationToken ct = default)
    {
        var query = _context.Vehicles.Where(v => v.PlateNumber == plateNumber);
        if (excludeVehicleId.HasValue)
        {
            query = query.Where(v => v.Id != excludeVehicleId.Value);
        }
        return await query.AnyAsync(ct);
    }

    public async Task<bool> VinExistsAsync(string vin, int? excludeVehicleId = null, CancellationToken ct = default)
    {
        var query = _context.Vehicles.Where(v => v.Vin == vin);
        if (excludeVehicleId.HasValue)
        {
            query = query.Where(v => v.Id != excludeVehicleId.Value);
        }
        return await query.AnyAsync(ct);
    }

    public async Task<int> GetCountByInvestorIdAsync(int investorId, CancellationToken ct = default)
    {
        return await _context.Vehicles.CountAsync(v => v.InvestorId == investorId, ct);
    }

    public async Task<IReadOnlyList<Vehicle>> GetAvailableAsync(CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Include(v => v.Investor)
            .Include(v => v.Status)
            .Include(v => v.VehicleType)
            .Include(v => v.Documents)
            .Where(v => v.Status.Code == "AVAILABLE")
            .OrderBy(v => v.PlateNumber)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Vehicle vehicle, CancellationToken ct = default)
    {
        await _context.Vehicles.AddAsync(vehicle, ct);
    }

    public void Update(Vehicle vehicle)
    {
        _context.Vehicles.Update(vehicle);
    }

    public void Delete(Vehicle vehicle)
    {
        _context.Vehicles.Remove(vehicle);
    }
}
