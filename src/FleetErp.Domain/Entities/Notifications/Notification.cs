using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Notifications;

/// <summary>
/// Represents a system notification for expiration alerts, due dates, etc.
/// </summary>
public class Notification : BaseEntity
{
    public int NotificationTypeId { get; set; }
    public int StatusId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? ReferenceType { get; set; }
    public int? ReferenceId { get; set; }
    public DateTime? DueAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public int? ReadByUserId { get; set; }
    public DateTime? DismissedAt { get; set; }
    public int? DismissedByUserId { get; set; }

    // Navigation properties
    public LookupItem NotificationType { get; set; } = null!;
    public LookupItem Status { get; set; } = null!;
}
