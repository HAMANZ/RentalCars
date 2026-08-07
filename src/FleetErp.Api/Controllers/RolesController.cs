using FleetErp.Api.Common;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Controllers;

public class RolesController : ApiControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Get all roles
    /// </summary>
    [HttpGet]
    [HasPermission(Permissions.Roles.View)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _roleService.GetAllAsync(ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get paginated list of roles
    /// </summary>
    [HttpGet("paged")]
    [HasPermission(Permissions.Roles.View)]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var result = await _roleService.GetPagedAsync(page, pageSize, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get role by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [HasPermission(Permissions.Roles.View)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _roleService.GetByIdAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new role
    /// </summary>
    [HttpPost]
    [HasPermission(Permissions.Roles.Create)]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request, CancellationToken ct)
    {
        var result = await _roleService.CreateAsync(request, ct);
        return result.ToCreatedResult(this, nameof(GetById), new { id = result.Value?.Id });
    }

    /// <summary>
    /// Update an existing role
    /// </summary>
    [HttpPut("{id:int}")]
    [HasPermission(Permissions.Roles.Edit)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleRequest request, CancellationToken ct)
    {
        var result = await _roleService.UpdateAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a role
    /// </summary>
    [HttpDelete("{id:int}")]
    [HasPermission(Permissions.Roles.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _roleService.DeleteAsync(id, ct);
        return result.ToActionResult(this);
    }
}
