namespace FleetErp.Application.Notifications.Dtos;

public record NotificationDto(
    int Id,
    int NotificationTypeId,
    string NotificationTypeName,
    int StatusId,
    string StatusName,
    string Title,
    string? Message,
    string? ReferenceType,
    int? ReferenceId,
    DateTime? DueAt,
    DateTime? ReadAt,
    int? ReadByUserId,
    DateTime? DismissedAt,
    int? DismissedByUserId,
    DateTime CreatedAt,
    bool IsRead,
    bool IsDismissed
);

public record NotificationSummaryDto(
    int Id,
    string NotificationTypeName,
    string Title,
    string? Message,
    DateTime? DueAt,
    DateTime CreatedAt,
    bool IsRead
);

public record CreateNotificationRequest(
    int NotificationTypeId,
    string Title,
    string? Message,
    string? ReferenceType,
    int? ReferenceId,
    DateTime? DueAt
);

public record NotificationCountDto(
    int Total,
    int Unread,
    int Today
);
