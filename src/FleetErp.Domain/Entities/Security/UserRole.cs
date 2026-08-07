namespace FleetErp.Domain.Entities.Security;

/// <summary>
/// Join entity for User-Role many-to-many relationship.
/// </summary>
public class UserRole
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public DateTime AssignedAt { get; set; }
    public string? AssignedBy { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
