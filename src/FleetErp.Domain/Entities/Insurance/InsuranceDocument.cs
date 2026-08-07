using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Insurance;

/// <summary>
/// Represents a document attached to an insurance record (policy document, claim form, etc.).
/// </summary>
public class InsuranceDocument : BaseEntity
{
    public int InsuranceRecordId { get; set; }
    public int DocumentTypeId { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? ExpiresAt { get; set; }

    // Navigation properties
    public InsuranceRecord InsuranceRecord { get; set; } = null!;
    public LookupItem DocumentType { get; set; } = null!;
}
