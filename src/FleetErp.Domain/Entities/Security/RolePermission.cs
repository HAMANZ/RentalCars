namespace FleetErp.Domain.Entities.Security;

/// <summary>
/// Join entity for Role-Permission many-to-many relationship.
/// </summary>
public class RolePermission
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    // Navigation properties
    public Role Role { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}
