using FleetErp.Domain.Common;

namespace FleetErp.Domain.Entities.Security;

/// <summary>
/// Dynamic menu item for sidebar navigation.
/// Menu visibility is controlled by the linked Permission.
/// </summary>
public class Menu : BaseEntity
{
    public int? ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Route { get; set; }
    public string? Icon { get; set; }
    public int SortOrder { get; set; }
    public int? PermissionId { get; set; }  // If set, user must have this permission to see the menu
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public Menu? Parent { get; set; }
    public Permission? Permission { get; set; }
    public ICollection<Menu> Children { get; set; } = [];
}
