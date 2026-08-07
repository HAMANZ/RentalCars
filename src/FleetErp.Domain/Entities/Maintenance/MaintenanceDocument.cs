using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Maintenance;

/// <summary>
/// Represents a document attached to a maintenance record (invoices, receipts, photos).
/// </summary>
public class MaintenanceDocument : BaseEntity
{
    public int MaintenanceRecordId { get; set; }
    public int DocumentTypeId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public MaintenanceRecord MaintenanceRecord { get; set; } = null!;
    public LookupItem DocumentType { get; set; } = null!;
}
