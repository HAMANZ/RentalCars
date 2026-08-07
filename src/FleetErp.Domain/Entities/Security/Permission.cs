using FleetErp.Domain.Common;

namespace FleetErp.Domain.Entities.Security;

public class Permission : BaseEntity
{
    public string Code { get; set; } = string.Empty;   // e.g., "Invoices.Create", "Payments.Approve"
    public string? Description { get; set; }
    public string? Module { get; set; }                 // e.g., "Invoices", "Payments" - for grouping in UI

    // Navigation properties
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
    public ICollection<Menu> Menus { get; set; } = [];
}
