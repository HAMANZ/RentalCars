using FleetErp.Application.Common;
using FleetErp.Application.Maintenance.Interfaces;
using FleetErp.Domain.Entities.Maintenance;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class MaintenanceRepository : IMaintenanceRepository
{
    private readonly FleetErpDbContext _context;

    public MaintenanceRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceRecord?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.MaintenanceRecords
            .Include(m => m.Vehicle)
            .Include(m => m.MaintenanceType)
            .Include(m => m.Status)
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }

    public async Task<MaintenanceRecord?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default)
    {
        return await _context.MaintenanceRecords
            .Include(m => m.Vehicle)
            .Include(m => m.MaintenanceType)
            .Include(m => m.Status)
            .Include(m => m.Documents)
                .ThenInclude(d => d.DocumentType)
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }

    public async Task<PagedResult<MaintenanceRecord>> GetPagedAsync(int page, int pageSize, string? search, int? vehicleId, int? statusId, int? maintenanceTypeId, CancellationToken ct = default)
    {
        var query = _context.MaintenanceRecords
            .Include(m => m.Vehicle)
            .Include(m => m.MaintenanceType)
            .Include(m => m.Status)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(m =>
                m.Vehicle.PlateNumber.Contains(search) ||
                m.Description!.Contains(search) ||
                m.ServiceProvider!.Contains(search));
        }

        if (vehicleId.HasValue)
        {
            query = query.Where(m => m.VehicleId == vehicleId.Value);
        }

        if (statusId.HasValue)
        {
            query = query.Where(m => m.StatusId == statusId.Value);
        }

        if (maintenanceTypeId.HasValue)
        {
            query = query.Where(m => m.MaintenanceTypeId == maintenanceTypeId.Value);
        }

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(m => m.ScheduledDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<MaintenanceRecord>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<IReadOnlyList<MaintenanceRecord>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default)
    {
        return await _context.MaintenanceRecords
            .Include(m => m.MaintenanceType)
            .Include(m => m.Status)
            .Where(m => m.VehicleId == vehicleId)
            .OrderByDescending(m => m.ScheduledDate)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<MaintenanceRecord>> GetUpcomingAsync(int days, CancellationToken ct = default)
    {
        var futureDate = DateTime.UtcNow.AddDays(days);
        return await _context.MaintenanceRecords
            .Include(m => m.Vehicle)
            .Include(m => m.MaintenanceType)
            .Include(m => m.Status)
            .Where(m => m.ScheduledDate <= futureDate && m.CompletedDate == null)
            .Where(m => m.Status.Code != "COMPLETED" && m.Status.Code != "CANCELLED")
            .OrderBy(m => m.ScheduledDate)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<MaintenanceRecord>> GetScheduledAsync(int days, CancellationToken ct = default)
    {
        var today = DateTime.UtcNow.Date;
        var futureDate = today.AddDays(days);
        return await _context.MaintenanceRecords
            .Include(m => m.Vehicle)
            .Include(m => m.MaintenanceType)
            .Include(m => m.Status)
            .Where(m => m.ScheduledDate >= today &&
                        m.ScheduledDate <= futureDate &&
                        m.CompletedDate == null)
            .Where(m => m.Status.Code != "COMPLETED" && m.Status.Code != "CANCELLED")
            .OrderBy(m => m.ScheduledDate)
            .ToListAsync(ct);
    }

    public async Task AddAsync(MaintenanceRecord record, CancellationToken ct = default)
    {
        await _context.MaintenanceRecords.AddAsync(record, ct);
    }

    public void Update(MaintenanceRecord record)
    {
        _context.MaintenanceRecords.Update(record);
    }

    public void Delete(MaintenanceRecord record)
    {
        record.IsDeleted = true;
        _context.MaintenanceRecords.Update(record);
    }
}
