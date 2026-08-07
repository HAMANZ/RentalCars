using FleetErp.Domain.Common;

namespace FleetErp.Domain.Entities.Security;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsSystem { get; set; }  // System roles (Admin) cannot be deleted

    // Navigation properties
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
}
