using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Maintenance;

namespace FleetErp.Application.Maintenance.Interfaces;

public interface IMaintenanceRepository
{
    Task<MaintenanceRecord?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<MaintenanceRecord?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default);
    Task<PagedResult<MaintenanceRecord>> GetPagedAsync(int page, int pageSize, string? search, int? vehicleId, int? statusId, int? maintenanceTypeId, CancellationToken ct = default);
    Task<IReadOnlyList<MaintenanceRecord>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default);
    Task<IReadOnlyList<MaintenanceRecord>> GetUpcomingAsync(int days, CancellationToken ct = default);
    Task<IReadOnlyList<MaintenanceRecord>> GetScheduledAsync(int days, CancellationToken ct = default);
    Task AddAsync(MaintenanceRecord record, CancellationToken ct = default);
    void Update(MaintenanceRecord record);
    void Delete(MaintenanceRecord record);
}
