using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Application.Maintenance.Interfaces;
using FleetErp.Application.Notifications.Dtos;
using FleetErp.Application.Notifications.Interfaces;
using FleetErp.Domain.Entities.Notifications;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILookupRepository _lookupRepository;
    private readonly IInsuranceRepository _insuranceRepository;
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(
        INotificationRepository notificationRepository,
        ILookupRepository lookupRepository,
        IInsuranceRepository insuranceRepository,
        IMaintenanceRepository maintenanceRepository,
        IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _lookupRepository = lookupRepository;
        _insuranceRepository = insuranceRepository;
        _maintenanceRepository = maintenanceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<NotificationDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var notification = await _notificationRepository.GetByIdAsync(id, ct);
        if (notification is null)
        {
            return Result<NotificationDto>.NotFound("Notification not found");
        }

        return Result<NotificationDto>.Success(MapToDto(notification));
    }

    public async Task<Result<PagedResult<NotificationDto>>> GetPagedAsync(int page, int pageSize, string? search, int? statusId, int? typeId, CancellationToken ct = default)
    {
        var result = await _notificationRepository.GetPagedAsync(page, pageSize, search, statusId, typeId, ct);

        var dtos = result.Items.Select(MapToDto).ToList();
        return Result<PagedResult<NotificationDto>>.Success(
            new PagedResult<NotificationDto> { Items = dtos, Page = result.Page, PageSize = result.PageSize, TotalCount = result.TotalCount });
    }

    public async Task<Result<IReadOnlyList<NotificationDto>>> GetUnreadAsync(CancellationToken ct = default)
    {
        var notifications = await _notificationRepository.GetUnreadAsync(ct);
        var dtos = notifications.Select(MapToDto).ToList();
        return Result<IReadOnlyList<NotificationDto>>.Success(dtos);
    }

    public async Task<Result<int>> GetUnreadCountAsync(CancellationToken ct = default)
    {
        var count = await _notificationRepository.GetUnreadCountAsync(ct);
        return Result<int>.Success(count);
    }

    public async Task<Result<NotificationDto>> CreateAsync(CreateNotificationRequest request, CancellationToken ct = default)
    {
        // Get pending status
        var pendingStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.NotificationStatus, "PENDING", ct);
        if (pendingStatus is null)
        {
            return Result<NotificationDto>.Invalid("Notification status 'PENDING' not found");
        }

        var notification = new Notification
        {
            NotificationTypeId = request.NotificationTypeId,
            StatusId = pendingStatus.Id,
            Title = request.Title,
            Message = request.Message,
            ReferenceType = request.ReferenceType,
            ReferenceId = request.ReferenceId,
            DueAt = request.DueAt
        };

        await _notificationRepository.AddAsync(notification, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        var created = await _notificationRepository.GetByIdAsync(notification.Id, ct);
        return Result<NotificationDto>.Success(MapToDto(created!));
    }

    public async Task<Result> MarkAsReadAsync(int id, int userId, CancellationToken ct = default)
    {
        var notification = await _notificationRepository.GetByIdAsync(id, ct);
        if (notification is null)
        {
            return Result.NotFound("Notification not found");
        }

        notification.ReadAt = DateTime.UtcNow;
        notification.ReadByUserId = userId;

        // Get read status
        var readStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.NotificationStatus, "READ", ct);
        if (readStatus is not null)
        {
            notification.StatusId = readStatus.Id;
        }

        _notificationRepository.Update(notification);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result> MarkAllAsReadAsync(int userId, CancellationToken ct = default)
    {
        var unreadNotifications = await _notificationRepository.GetUnreadAsync(ct);

        var readStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.NotificationStatus, "READ", ct);

        foreach (var notification in unreadNotifications)
        {
            notification.ReadAt = DateTime.UtcNow;
            notification.ReadByUserId = userId;
            if (readStatus is not null)
            {
                notification.StatusId = readStatus.Id;
            }
            _notificationRepository.Update(notification);
        }

        await _unitOfWork.SaveChangesAsync(ct);
        return Result.Success();
    }

    public async Task<Result> DismissAsync(int id, int userId, CancellationToken ct = default)
    {
        var notification = await _notificationRepository.GetByIdAsync(id, ct);
        if (notification is null)
        {
            return Result.NotFound("Notification not found");
        }

        notification.DismissedAt = DateTime.UtcNow;
        notification.DismissedByUserId = userId;

        // Get dismissed status
        var dismissedStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.NotificationStatus, "DISMISSED", ct);
        if (dismissedStatus is not null)
        {
            notification.StatusId = dismissedStatus.Id;
        }

        _notificationRepository.Update(notification);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var notification = await _notificationRepository.GetByIdAsync(id, ct);
        if (notification is null)
        {
            return Result.NotFound("Notification not found");
        }

        _notificationRepository.Delete(notification);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task GenerateExpirationNotificationsAsync(CancellationToken ct = default)
    {
        // Get lookup types for notifications
        var insuranceExpiryType = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.NotificationType, "INSURANCE_EXPIRY", ct);
        var maintenanceDueType = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.NotificationType, "MAINTENANCE_DUE", ct);
        var pendingStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.NotificationStatus, "PENDING", ct);

        if (pendingStatus is null) return;

        var notifications = new List<Notification>();

        // Check insurance expirations (30 days ahead)
        if (insuranceExpiryType is not null)
        {
            var expiringInsurance = await _insuranceRepository.GetExpiringAsync(30, ct);
            foreach (var insurance in expiringInsurance)
            {
                // Check if notification already exists
                var exists = await _notificationRepository.ExistsAsync("InsuranceRecord", insurance.Id, insuranceExpiryType.Id, ct);
                if (!exists)
                {
                    notifications.Add(new Notification
                    {
                        NotificationTypeId = insuranceExpiryType.Id,
                        StatusId = pendingStatus.Id,
                        Title = $"Insurance expiring: {insurance.PolicyNumber}",
                        Message = $"Insurance for {insurance.Vehicle.PlateNumber} expires on {insurance.EndDate:MMM dd, yyyy}",
                        ReferenceType = "InsuranceRecord",
                        ReferenceId = insurance.Id,
                        DueAt = insurance.EndDate
                    });
                }
            }
        }

        // Check maintenance due (based on scheduled date within 7 days)
        if (maintenanceDueType is not null)
        {
            var upcomingMaintenance = await _maintenanceRepository.GetScheduledAsync(7, ct);
            foreach (var maintenance in upcomingMaintenance)
            {
                var exists = await _notificationRepository.ExistsAsync("MaintenanceRecord", maintenance.Id, maintenanceDueType.Id, ct);
                if (!exists)
                {
                    notifications.Add(new Notification
                    {
                        NotificationTypeId = maintenanceDueType.Id,
                        StatusId = pendingStatus.Id,
                        Title = $"Maintenance due: {maintenance.Vehicle.PlateNumber}",
                        Message = $"{maintenance.MaintenanceType.Name} scheduled for {maintenance.ScheduledDate:MMM dd, yyyy}",
                        ReferenceType = "MaintenanceRecord",
                        ReferenceId = maintenance.Id,
                        DueAt = maintenance.ScheduledDate
                    });
                }
            }
        }

        if (notifications.Count > 0)
        {
            await _notificationRepository.AddRangeAsync(notifications, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }

    private static NotificationDto MapToDto(Notification n)
    {
        return new NotificationDto(
            n.Id,
            n.NotificationTypeId,
            n.NotificationType.Name,
            n.StatusId,
            n.Status.Name,
            n.Title,
            n.Message,
            n.ReferenceType,
            n.ReferenceId,
            n.DueAt,
            n.ReadAt,
            n.ReadByUserId,
            n.DismissedAt,
            n.DismissedByUserId,
            n.CreatedAt,
            n.ReadAt.HasValue,
            n.DismissedAt.HasValue
        );
    }
}
