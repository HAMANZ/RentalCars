using FleetErp.Application.Common;
using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class InsuranceRepository : IInsuranceRepository
{
    private readonly FleetErpDbContext _context;

    public InsuranceRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<InsuranceRecord?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.InsuranceRecords
            .Include(i => i.Vehicle)
            .Include(i => i.InsuranceCompany)
            .Include(i => i.InsuranceType)
            .Include(i => i.Status)
            .FirstOrDefaultAsync(i => i.Id == id, ct);
    }

    public async Task<InsuranceRecord?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default)
    {
        return await _context.InsuranceRecords
            .Include(i => i.Vehicle)
            .Include(i => i.InsuranceCompany)
            .Include(i => i.InsuranceType)
            .Include(i => i.Status)
            .Include(i => i.Documents)
            .FirstOrDefaultAsync(i => i.Id == id, ct);
    }

    public async Task<PagedResult<InsuranceRecord>> GetPagedAsync(int page, int pageSize, string? search, int? vehicleId, int? statusId, int? insuranceTypeId, int? companyId, CancellationToken ct = default)
    {
        var query = _context.InsuranceRecords
            .Include(i => i.Vehicle)
            .Include(i => i.InsuranceCompany)
            .Include(i => i.InsuranceType)
            .Include(i => i.Status)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(i =>
                i.PolicyNumber.Contains(search) ||
                i.Vehicle.PlateNumber.Contains(search) ||
                i.InsuranceCompany.Name.Contains(search));
        }

        if (vehicleId.HasValue)
        {
            query = query.Where(i => i.VehicleId == vehicleId.Value);
        }

        if (statusId.HasValue)
        {
            query = query.Where(i => i.StatusId == statusId.Value);
        }

        if (insuranceTypeId.HasValue)
        {
            query = query.Where(i => i.InsuranceTypeId == insuranceTypeId.Value);
        }

        if (companyId.HasValue)
        {
            query = query.Where(i => i.InsuranceCompanyId == companyId.Value);
        }

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .OrderByDescending(i => i.EndDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<InsuranceRecord>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<IReadOnlyList<InsuranceRecord>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default)
    {
        return await _context.InsuranceRecords
            .Include(i => i.InsuranceCompany)
            .Include(i => i.InsuranceType)
            .Include(i => i.Status)
            .Where(i => i.VehicleId == vehicleId)
            .OrderByDescending(i => i.EndDate)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<InsuranceRecord>> GetExpiringAsync(int days, CancellationToken ct = default)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(days);
        var today = DateTime.UtcNow.Date;

        return await _context.InsuranceRecords
            .Include(i => i.Vehicle)
            .Include(i => i.InsuranceCompany)
            .Include(i => i.InsuranceType)
            .Include(i => i.Status)
            .Where(i => i.EndDate >= today && i.EndDate <= cutoffDate)
            .Where(i => i.Status.Code != "CANCELLED" && i.Status.Code != "EXPIRED")
            .OrderBy(i => i.EndDate)
            .ToListAsync(ct);
    }

    public async Task<InsuranceRecord?> GetActiveByVehicleIdAsync(int vehicleId, CancellationToken ct = default)
    {
        var today = DateTime.UtcNow.Date;

        return await _context.InsuranceRecords
            .Include(i => i.InsuranceCompany)
            .Include(i => i.InsuranceType)
            .Include(i => i.Status)
            .Where(i => i.VehicleId == vehicleId)
            .Where(i => i.StartDate <= today && i.EndDate >= today)
            .Where(i => i.Status.Code == "ACTIVE")
            .OrderByDescending(i => i.EndDate)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ExistsByPolicyNumberAsync(string policyNumber, int? excludeId = null, CancellationToken ct = default)
    {
        var query = _context.InsuranceRecords.Where(i => i.PolicyNumber == policyNumber);

        if (excludeId.HasValue)
        {
            query = query.Where(i => i.Id != excludeId.Value);
        }

        return await query.AnyAsync(ct);
    }

    public async Task AddAsync(InsuranceRecord record, CancellationToken ct = default)
    {
        await _context.InsuranceRecords.AddAsync(record, ct);
    }

    public void Update(InsuranceRecord record)
    {
        _context.InsuranceRecords.Update(record);
    }

    public void Delete(InsuranceRecord record)
    {
        record.IsDeleted = true;
    }
}
