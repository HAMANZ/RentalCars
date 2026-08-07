using FleetErp.Api.Common;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get paginated list of users
    /// </summary>
    [HttpGet]
    [HasPermission(Permissions.Users.View)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        CancellationToken ct = default)
    {
        var result = await _userService.GetPagedAsync(page, pageSize, search, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [HasPermission(Permissions.Users.View)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _userService.GetByIdAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    [HttpPost]
    [HasPermission(Permissions.Users.Create)]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct)
    {
        var result = await _userService.CreateAsync(request, ct);
        return result.ToCreatedResult(this, nameof(GetById), new { id = result.Value?.Id });
    }

    /// <summary>
    /// Update an existing user
    /// </summary>
    [HttpPut("{id:int}")]
    [HasPermission(Permissions.Users.Edit)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request, CancellationToken ct)
    {
        var result = await _userService.UpdateAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a user (soft delete)
    /// </summary>
    [HttpDelete("{id:int}")]
    [HasPermission(Permissions.Users.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _userService.DeleteAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Activate a user
    /// </summary>
    [HttpPost("{id:int}/activate")]
    [HasPermission(Permissions.Users.Edit)]
    public async Task<IActionResult> Activate(int id, CancellationToken ct)
    {
        var result = await _userService.ActivateAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Deactivate a user
    /// </summary>
    [HttpPost("{id:int}/deactivate")]
    [HasPermission(Permissions.Users.Edit)]
    public async Task<IActionResult> Deactivate(int id, CancellationToken ct)
    {
        var result = await _userService.DeactivateAsync(id, ct);
        return result.ToActionResult(this);
    }
}
