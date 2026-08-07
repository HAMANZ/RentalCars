using FleetErp.Application.Common;
using FleetErp.Application.Notifications.Interfaces;
using FleetErp.Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly FleetErpDbContext _context;

    public NotificationRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Notification?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Notifications
            .Include(n => n.NotificationType)
            .Include(n => n.Status)
            .FirstOrDefaultAsync(n => n.Id == id, ct);
    }

    public async Task<PagedResult<Notification>> GetPagedAsync(int page, int pageSize, string? search, int? statusId, int? typeId, CancellationToken ct = default)
    {
        var query = _context.Notifications
            .Include(n => n.NotificationType)
            .Include(n => n.Status)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(n =>
                n.Title.Contains(search) ||
                (n.Message != null && n.Message.Contains(search)));
        }

        if (statusId.HasValue)
        {
            query = query.Where(n => n.StatusId == statusId.Value);
        }

        if (typeId.HasValue)
        {
            query = query.Where(n => n.NotificationTypeId == typeId.Value);
        }

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .OrderByDescending(n => n.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<Notification>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<IReadOnlyList<Notification>> GetUnreadAsync(CancellationToken ct = default)
    {
        return await _context.Notifications
            .Include(n => n.NotificationType)
            .Include(n => n.Status)
            .Where(n => n.ReadAt == null && n.DismissedAt == null)
            .OrderByDescending(n => n.CreatedAt)
            .Take(50)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Notification>> GetByReferenceAsync(string referenceType, int referenceId, CancellationToken ct = default)
    {
        return await _context.Notifications
            .Include(n => n.NotificationType)
            .Include(n => n.Status)
            .Where(n => n.ReferenceType == referenceType && n.ReferenceId == referenceId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<int> GetUnreadCountAsync(CancellationToken ct = default)
    {
        return await _context.Notifications
            .Where(n => n.ReadAt == null && n.DismissedAt == null)
            .CountAsync(ct);
    }

    public async Task<bool> ExistsAsync(string referenceType, int referenceId, int notificationTypeId, CancellationToken ct = default)
    {
        return await _context.Notifications
            .AnyAsync(n =>
                n.ReferenceType == referenceType &&
                n.ReferenceId == referenceId &&
                n.NotificationTypeId == notificationTypeId &&
                n.DismissedAt == null, ct);
    }

    public async Task AddAsync(Notification notification, CancellationToken ct = default)
    {
        await _context.Notifications.AddAsync(notification, ct);
    }

    public async Task AddRangeAsync(IEnumerable<Notification> notifications, CancellationToken ct = default)
    {
        await _context.Notifications.AddRangeAsync(notifications, ct);
    }

    public void Update(Notification notification)
    {
        _context.Notifications.Update(notification);
    }

    public void Delete(Notification notification)
    {
        notification.IsDeleted = true;
    }
}
