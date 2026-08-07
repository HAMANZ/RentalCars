using FleetErp.Domain.Common;

namespace FleetErp.Domain.Entities.Audit;

/// <summary>
/// Audit log entry for tracking all mutations in the system.
/// Every create/update/delete/status-change writes an entry here.
/// </summary>
public class AuditLog : BaseEntity
{
    public string Action { get; set; } = string.Empty;        // e.g., "Created", "Updated", "Deleted", "StatusChanged"
    public string EntityType { get; set; } = string.Empty;    // e.g., "Invoice", "Payment", "Contract"
    public int EntityId { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Details { get; set; }                      // JSON or free text description
    public string? OldValues { get; set; }                    // JSON of old values (optional)
    public string? NewValues { get; set; }                    // JSON of new values (optional)
    public string? IpAddress { get; set; }
    public DateTime Timestamp { get; set; }
}
