using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Customers;

public class CustomerDocument : BaseEntity
{
    public int CustomerId { get; set; }
    public int DocumentTypeId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }

    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

    // Navigation properties
    public Customer Customer { get; set; } = null!;
    public LookupItem DocumentType { get; set; } = null!;
}
