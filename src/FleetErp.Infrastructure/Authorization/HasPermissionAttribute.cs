using Microsoft.AspNetCore.Authorization;

namespace FleetErp.Infrastructure.Authorization;

/// <summary>
/// Requires the user to have the specified permission to access the endpoint.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) : base(permission)
    {
    }
}
