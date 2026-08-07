using FleetErp.Application.Accounting.Interfaces;
using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Application.Maintenance.Dtos;
using FleetErp.Application.Maintenance.Interfaces;
using FleetErp.Domain.Entities.Maintenance;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Services;

public class MaintenanceService : IMaintenanceService
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly ILookupRepository _lookupRepository;
    private readonly IAuditLogger _auditLogger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionEngine _transactionEngine;

    public MaintenanceService(
        IMaintenanceRepository maintenanceRepository,
        ILookupRepository lookupRepository,
        IAuditLogger auditLogger,
        IUnitOfWork unitOfWork,
        ITransactionEngine transactionEngine)
    {
        _maintenanceRepository = maintenanceRepository;
        _lookupRepository = lookupRepository;
        _auditLogger = auditLogger;
        _unitOfWork = unitOfWork;
        _transactionEngine = transactionEngine;
    }

    public async Task<Result<MaintenanceRecordDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var record = await _maintenanceRepository.GetByIdWithDocumentsAsync(id, ct);
        if (record is null)
        {
            return Result<MaintenanceRecordDto>.NotFound("Maintenance record not found");
        }

        return Result<MaintenanceRecordDto>.Success(MapToDto(record));
    }

    public async Task<Result<PagedResult<MaintenanceRecordListDto>>> GetPagedAsync(int page, int pageSize, string? search, int? vehicleId, int? statusId, int? maintenanceTypeId, CancellationToken ct = default)
    {
        var result = await _maintenanceRepository.GetPagedAsync(page, pageSize, search, vehicleId, statusId, maintenanceTypeId, ct);

        var dtos = result.Items.Select(MapToListDto).ToList();
        return Result<PagedResult<MaintenanceRecordListDto>>.Success(
            new PagedResult<MaintenanceRecordListDto> { Items = dtos, Page = result.Page, PageSize = result.PageSize, TotalCount = result.TotalCount });
    }

    public async Task<Result<IReadOnlyList<MaintenanceRecordListDto>>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default)
    {
        var records = await _maintenanceRepository.GetByVehicleIdAsync(vehicleId, ct);
        var dtos = records.Select(MapToListDto).ToList();
        return Result<IReadOnlyList<MaintenanceRecordListDto>>.Success(dtos);
    }

    public async Task<Result<IReadOnlyList<MaintenanceRecordListDto>>> GetUpcomingAsync(int days, CancellationToken ct = default)
    {
        var records = await _maintenanceRepository.GetUpcomingAsync(days, ct);
        var dtos = records.Select(MapToListDto).ToList();
        return Result<IReadOnlyList<MaintenanceRecordListDto>>.Success(dtos);
    }

    public async Task<Result<MaintenanceRecordDto>> CreateAsync(CreateMaintenanceRequest request, CancellationToken ct = default)
    {
        var record = new MaintenanceRecord
        {
            VehicleId = request.VehicleId,
            MaintenanceTypeId = request.MaintenanceTypeId,
            StatusId = request.StatusId,
            Description = request.Description,
            Cost = request.Cost,
            ScheduledDate = request.ScheduledDate,
            OdometerAtService = request.OdometerAtService,
            ServiceProvider = request.ServiceProvider,
            Notes = request.Notes,
            IsExpensePosted = false
        };

        await _maintenanceRepository.AddAsync(record, ct);
        _auditLogger.Log(AuditActions.MaintenanceCreated, nameof(MaintenanceRecord), record.Id,
            userId: null, $"Maintenance record created for vehicle ID {request.VehicleId}");

        await _unitOfWork.SaveChangesAsync(ct);

        var created = await _maintenanceRepository.GetByIdAsync(record.Id, ct);
        return Result<MaintenanceRecordDto>.Success(MapToDto(created!));
    }

    public async Task<Result<MaintenanceRecordDto>> UpdateAsync(int id, UpdateMaintenanceRequest request, CancellationToken ct = default)
    {
        var record = await _maintenanceRepository.GetByIdAsync(id, ct);
        if (record is null)
        {
            return Result<MaintenanceRecordDto>.NotFound("Maintenance record not found");
        }

        // Cannot update completed or cancelled records
        if (record.Status.Code == "COMPLETED" || record.Status.Code == "CANCELLED")
        {
            return Result<MaintenanceRecordDto>.Invalid("Cannot update a completed or cancelled maintenance record");
        }

        record.MaintenanceTypeId = request.MaintenanceTypeId;
        record.StatusId = request.StatusId;
        record.Description = request.Description;
        record.Cost = request.Cost;
        record.ScheduledDate = request.ScheduledDate;
        record.OdometerAtService = request.OdometerAtService;
        record.ServiceProvider = request.ServiceProvider;
        record.Notes = request.Notes;

        _maintenanceRepository.Update(record);
        _auditLogger.Log(AuditActions.MaintenanceUpdated, nameof(MaintenanceRecord), record.Id,
            userId: null, $"Maintenance record updated");

        await _unitOfWork.SaveChangesAsync(ct);

        var updated = await _maintenanceRepository.GetByIdAsync(record.Id, ct);
        return Result<MaintenanceRecordDto>.Success(MapToDto(updated!));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var record = await _maintenanceRepository.GetByIdAsync(id, ct);
        if (record is null)
        {
            return Result.NotFound("Maintenance record not found");
        }

        // Cannot delete if expense has been posted
        if (record.IsExpensePosted)
        {
            return Result.Invalid("Cannot delete a maintenance record with posted expenses");
        }

        _maintenanceRepository.Delete(record);
        _auditLogger.Log(AuditActions.MaintenanceDeleted, nameof(MaintenanceRecord), record.Id,
            userId: null, $"Maintenance record deleted");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result<MaintenanceRecordDto>> StartAsync(int id, CancellationToken ct = default)
    {
        var record = await _maintenanceRepository.GetByIdAsync(id, ct);
        if (record is null)
        {
            return Result<MaintenanceRecordDto>.NotFound("Maintenance record not found");
        }

        if (record.Status.Code != "SCHEDULED")
        {
            return Result<MaintenanceRecordDto>.Invalid("Only scheduled maintenance can be started");
        }

        // Get in-progress status
        var inProgressStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.MaintenanceStatus, "IN_PROGRESS", ct);
        if (inProgressStatus is null)
        {
            return Result<MaintenanceRecordDto>.Invalid("Maintenance status 'IN_PROGRESS' not found");
        }

        record.StatusId = inProgressStatus.Id;

        _maintenanceRepository.Update(record);
        _auditLogger.Log(AuditActions.MaintenanceStarted, nameof(MaintenanceRecord), record.Id,
            userId: null, $"Maintenance started");

        await _unitOfWork.SaveChangesAsync(ct);

        var updated = await _maintenanceRepository.GetByIdAsync(record.Id, ct);
        return Result<MaintenanceRecordDto>.Success(MapToDto(updated!));
    }

    public async Task<Result<MaintenanceRecordDto>> CompleteAsync(int id, CompleteMaintenanceRequest request, CancellationToken ct = default)
    {
        var record = await _maintenanceRepository.GetByIdAsync(id, ct);
        if (record is null)
        {
            return Result<MaintenanceRecordDto>.NotFound("Maintenance record not found");
        }

        if (record.Status.Code == "COMPLETED" || record.Status.Code == "CANCELLED")
        {
            return Result<MaintenanceRecordDto>.Invalid("Maintenance is already completed or cancelled");
        }

        // Get completed status
        var completedStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.MaintenanceStatus, "COMPLETED", ct);
        if (completedStatus is null)
        {
            return Result<MaintenanceRecordDto>.Invalid("Maintenance status 'COMPLETED' not found");
        }

        record.StatusId = completedStatus.Id;
        record.CompletedDate = request.CompletedDate;
        record.Cost = request.FinalCost;
        if (request.OdometerAtService.HasValue)
        {
            record.OdometerAtService = request.OdometerAtService;
        }
        if (!string.IsNullOrWhiteSpace(request.Notes))
        {
            record.Notes = request.Notes;
        }

        _maintenanceRepository.Update(record);
        _auditLogger.Log(AuditActions.MaintenanceCompleted, nameof(MaintenanceRecord), record.Id,
            userId: null, $"Maintenance completed with cost {request.FinalCost:C}");

        await _unitOfWork.SaveChangesAsync(ct);

        // Post expense transaction if cost > 0
        if (request.FinalCost > 0 && !record.IsExpensePosted)
        {
            await _transactionEngine.PostMaintenanceExpenseAsync(record.Id, record.VehicleId, request.FinalCost, ct);
            record.IsExpensePosted = true;
            _maintenanceRepository.Update(record);
            _auditLogger.Log(AuditActions.MaintenanceExpensePosted, nameof(MaintenanceRecord), record.Id,
                userId: null, $"Maintenance expense posted: {request.FinalCost:C}");
            await _unitOfWork.SaveChangesAsync(ct);
        }

        var updated = await _maintenanceRepository.GetByIdAsync(record.Id, ct);
        return Result<MaintenanceRecordDto>.Success(MapToDto(updated!));
    }

    public async Task<Result<MaintenanceRecordDto>> CancelAsync(int id, CancellationToken ct = default)
    {
        var record = await _maintenanceRepository.GetByIdAsync(id, ct);
        if (record is null)
        {
            return Result<MaintenanceRecordDto>.NotFound("Maintenance record not found");
        }

        if (record.Status.Code == "COMPLETED" || record.Status.Code == "CANCELLED")
        {
            return Result<MaintenanceRecordDto>.Invalid("Maintenance is already completed or cancelled");
        }

        // Cannot cancel if expense has been posted
        if (record.IsExpensePosted)
        {
            return Result<MaintenanceRecordDto>.Invalid("Cannot cancel a maintenance record with posted expenses");
        }

        // Get cancelled status
        var cancelledStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.MaintenanceStatus, "CANCELLED", ct);
        if (cancelledStatus is null)
        {
            return Result<MaintenanceRecordDto>.Invalid("Maintenance status 'CANCELLED' not found");
        }

        record.StatusId = cancelledStatus.Id;

        _maintenanceRepository.Update(record);
        _auditLogger.Log(AuditActions.MaintenanceCancelled, nameof(MaintenanceRecord), record.Id,
            userId: null, $"Maintenance cancelled");

        await _unitOfWork.SaveChangesAsync(ct);

        var updated = await _maintenanceRepository.GetByIdAsync(record.Id, ct);
        return Result<MaintenanceRecordDto>.Success(MapToDto(updated!));
    }

    private static MaintenanceRecordDto MapToDto(MaintenanceRecord record)
    {
        return new MaintenanceRecordDto(
            record.Id,
            record.VehicleId,
            record.Vehicle.PlateNumber,
            $"{record.Vehicle.Make} {record.Vehicle.Model} ({record.Vehicle.Year})",
            record.MaintenanceTypeId,
            record.MaintenanceType.Name,
            record.StatusId,
            record.Status.Name,
            record.Description,
            record.Cost,
            record.ScheduledDate,
            record.CompletedDate,
            record.OdometerAtService,
            record.ServiceProvider,
            record.Notes,
            record.IsExpensePosted,
            record.CreatedAt,
            record.UpdatedAt,
            record.Documents.Count
        );
    }

    private static MaintenanceRecordListDto MapToListDto(MaintenanceRecord record)
    {
        return new MaintenanceRecordListDto(
            record.Id,
            record.Vehicle.PlateNumber,
            $"{record.Vehicle.Make} {record.Vehicle.Model}",
            record.MaintenanceType.Name,
            record.Status.Name,
            record.Cost,
            record.ScheduledDate,
            record.CompletedDate,
            record.ServiceProvider
        );
    }
}
