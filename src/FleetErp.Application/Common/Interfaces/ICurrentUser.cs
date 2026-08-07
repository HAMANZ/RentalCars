namespace FleetErp.Application.Common.Interfaces;

/// <summary>
/// Abstraction for accessing the current authenticated user.
/// Implemented in the Api layer using HttpContext claims.
/// </summary>
public interface ICurrentUser
{
    string? UserId { get; }
    string? UserName { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Permissions { get; }
    bool HasPermission(string permission);
}
