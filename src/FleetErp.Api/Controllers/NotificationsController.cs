using FleetErp.Application.Notifications.Dtos;
using FleetErp.Application.Notifications.Interfaces;
using FleetErp.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FleetErp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] int? statusId = null,
        [FromQuery] int? typeId = null,
        CancellationToken ct = default)
    {
        var result = await _notificationService.GetPagedAsync(page, pageSize, search, statusId, typeId, ct);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("unread")]
    public async Task<IActionResult> GetUnread(CancellationToken ct = default)
    {
        var result = await _notificationService.GetUnreadAsync(ct);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount(CancellationToken ct = default)
    {
        var result = await _notificationService.GetUnreadCountAsync(ct);
        return result.IsSuccess ? Ok(new { count = result.Value }) : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct = default)
    {
        var result = await _notificationService.GetByIdAsync(id, ct);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost]
    [HasPermission("Notifications.Create")]
    public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request, CancellationToken ct = default)
    {
        var result = await _notificationService.CreateAsync(request, ct);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id, CancellationToken ct = default)
    {
        var userId = GetCurrentUserId();
        var result = await _notificationService.MarkAsReadAsync(id, userId, ct);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpPost("read-all")]
    public async Task<IActionResult> MarkAllAsRead(CancellationToken ct = default)
    {
        var userId = GetCurrentUserId();
        var result = await _notificationService.MarkAllAsReadAsync(userId, ct);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpPost("{id}/dismiss")]
    public async Task<IActionResult> Dismiss(int id, CancellationToken ct = default)
    {
        var userId = GetCurrentUserId();
        var result = await _notificationService.DismissAsync(id, userId, ct);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    [HasPermission("Notifications.Delete")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
    {
        var result = await _notificationService.DeleteAsync(id, ct);
        return result.IsSuccess ? NoContent() : NotFound(result.Error);
    }

    [HttpPost("generate")]
    [HasPermission("Notifications.Create")]
    public async Task<IActionResult> GenerateExpirationNotifications(CancellationToken ct = default)
    {
        await _notificationService.GenerateExpirationNotificationsAsync(ct);
        return Ok(new { message = "Expiration notifications generated" });
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(userIdClaim, out var userId) ? userId : 0;
    }
}
