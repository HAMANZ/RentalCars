namespace FleetErp.Application.Maintenance.Dtos;

public record MaintenanceRecordDto(
    int Id,
    int VehicleId,
    string VehiclePlateNumber,
    string VehicleDescription,
    int MaintenanceTypeId,
    string MaintenanceTypeName,
    int StatusId,
    string StatusName,
    string? Description,
    decimal Cost,
    DateTime ScheduledDate,
    DateTime? CompletedDate,
    int? OdometerAtService,
    string? ServiceProvider,
    string? Notes,
    bool IsExpensePosted,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int DocumentCount
);

public record MaintenanceRecordListDto(
    int Id,
    string VehiclePlateNumber,
    string VehicleDescription,
    string MaintenanceTypeName,
    string StatusName,
    decimal Cost,
    DateTime ScheduledDate,
    DateTime? CompletedDate,
    string? ServiceProvider
);

public record CreateMaintenanceRequest(
    int VehicleId,
    int MaintenanceTypeId,
    int StatusId,
    string? Description,
    decimal Cost,
    DateTime ScheduledDate,
    int? OdometerAtService,
    string? ServiceProvider,
    string? Notes
);

public record UpdateMaintenanceRequest(
    int MaintenanceTypeId,
    int StatusId,
    string? Description,
    decimal Cost,
    DateTime ScheduledDate,
    int? OdometerAtService,
    string? ServiceProvider,
    string? Notes
);

public record CompleteMaintenanceRequest(
    DateTime CompletedDate,
    decimal FinalCost,
    int? OdometerAtService,
    string? Notes
);
