using FleetErp.Domain.Common;

namespace FleetErp.Domain.Entities.Lookups;

/// <summary>
/// Generic lookup item for dynamic system configuration.
/// Used for statuses, types, categories that admins can manage at runtime.
/// Examples: InvoiceStatus, FuelType, PaymentMethod, ExpenseCategory, etc.
/// </summary>
public class LookupItem : BaseEntity
{
    public string LookupType { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsSystem { get; set; }  // System lookups cannot be deleted by users
}
