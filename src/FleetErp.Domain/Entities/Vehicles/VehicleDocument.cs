using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Vehicles;

/// <summary>
/// Represents a document uploaded for a vehicle (registration, insurance card, etc.)
/// </summary>
public class VehicleDocument : BaseEntity
{
    public int VehicleId { get; set; }
    public int DocumentTypeId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }

    // Computed property
    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

    // Navigation properties
    public Vehicle Vehicle { get; set; } = null!;
    public LookupItem DocumentType { get; set; } = null!;
}
