using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Investors;

/// <summary>
/// Represents a document uploaded for an investor (ID, contract, etc.)
/// </summary>
public class InvestorDocument : BaseEntity
{
    public int InvestorId { get; set; }
    public int DocumentTypeId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }

    // Computed property
    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

    // Navigation properties
    public Investor Investor { get; set; } = null!;
    public LookupItem DocumentType { get; set; } = null!;
}
