using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Notifications;

namespace FleetErp.Application.Notifications.Interfaces;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PagedResult<Notification>> GetPagedAsync(int page, int pageSize, string? search, int? statusId, int? typeId, CancellationToken ct = default);
    Task<IReadOnlyList<Notification>> GetUnreadAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Notification>> GetByReferenceAsync(string referenceType, int referenceId, CancellationToken ct = default);
    Task<int> GetUnreadCountAsync(CancellationToken ct = default);
    Task<bool> ExistsAsync(string referenceType, int referenceId, int notificationTypeId, CancellationToken ct = default);
    Task AddAsync(Notification notification, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<Notification> notifications, CancellationToken ct = default);
    void Update(Notification notification);
    void Delete(Notification notification);
}
