using FleetErp.Api.Common;
using FleetErp.Application.Security.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Controllers;

public class PermissionsController : ApiControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionsController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    /// <summary>
    /// Get all permissions
    /// </summary>
    [HttpGet]
    [HasPermission(Permissions.Roles.View)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _permissionService.GetAllAsync(ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get permissions grouped by module
    /// </summary>
    [HttpGet("grouped")]
    [HasPermission(Permissions.Roles.View)]
    public async Task<IActionResult> GetGrouped(CancellationToken ct)
    {
        var result = await _permissionService.GetGroupedAsync(ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get permissions for a specific role
    /// </summary>
    [HttpGet("role/{roleId:int}")]
    [HasPermission(Permissions.Roles.View)]
    public async Task<IActionResult> GetByRoleId(int roleId, CancellationToken ct)
    {
        var result = await _permissionService.GetByRoleIdAsync(roleId, ct);
        return result.ToActionResult(this);
    }
}
