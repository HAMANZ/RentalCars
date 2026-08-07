using FleetErp.Application.Common;
using FleetErp.Application.Maintenance.Dtos;

namespace FleetErp.Application.Maintenance.Interfaces;

public interface IMaintenanceService
{
    Task<Result<MaintenanceRecordDto>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<PagedResult<MaintenanceRecordListDto>>> GetPagedAsync(int page, int pageSize, string? search, int? vehicleId, int? statusId, int? maintenanceTypeId, CancellationToken ct = default);
    Task<Result<IReadOnlyList<MaintenanceRecordListDto>>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default);
    Task<Result<IReadOnlyList<MaintenanceRecordListDto>>> GetUpcomingAsync(int days, CancellationToken ct = default);
    Task<Result<MaintenanceRecordDto>> CreateAsync(CreateMaintenanceRequest request, CancellationToken ct = default);
    Task<Result<MaintenanceRecordDto>> UpdateAsync(int id, UpdateMaintenanceRequest request, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);

    // Status transitions
    Task<Result<MaintenanceRecordDto>> StartAsync(int id, CancellationToken ct = default);
    Task<Result<MaintenanceRecordDto>> CompleteAsync(int id, CompleteMaintenanceRequest request, CancellationToken ct = default);
    Task<Result<MaintenanceRecordDto>> CancelAsync(int id, CancellationToken ct = default);
}
