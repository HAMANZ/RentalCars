using FleetErp.Application.Common;
using FleetErp.Application.Notifications.Dtos;

namespace FleetErp.Application.Notifications.Interfaces;

public interface INotificationService
{
    Task<Result<NotificationDto>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<PagedResult<NotificationDto>>> GetPagedAsync(int page, int pageSize, string? search, int? statusId, int? typeId, CancellationToken ct = default);
    Task<Result<IReadOnlyList<NotificationDto>>> GetUnreadAsync(CancellationToken ct = default);
    Task<Result<int>> GetUnreadCountAsync(CancellationToken ct = default);
    Task<Result<NotificationDto>> CreateAsync(CreateNotificationRequest request, CancellationToken ct = default);
    Task<Result> MarkAsReadAsync(int id, int userId, CancellationToken ct = default);
    Task<Result> MarkAllAsReadAsync(int userId, CancellationToken ct = default);
    Task<Result> DismissAsync(int id, int userId, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);

    // Notification generation methods
    Task GenerateExpirationNotificationsAsync(CancellationToken ct = default);
}
