using System.Security.Claims;
using FleetErp.Application.Common.Interfaces;

namespace FleetErp.Api.Common;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public string? UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserName => User?.FindFirstValue(ClaimTypes.Name);

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public IEnumerable<string> Permissions
    {
        get
        {
            // In Phase 1, we'll load permissions from the database
            // For now, return permissions from claims
            return User?.Claims
                .Where(c => c.Type == "permission")
                .Select(c => c.Value) ?? [];
        }
    }

    public bool HasPermission(string permission)
    {
        return Permissions.Contains(permission);
    }
}
