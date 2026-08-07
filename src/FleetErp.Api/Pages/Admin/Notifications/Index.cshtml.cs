using FleetErp.Application.Notifications.Dtos;
using FleetErp.Application.Notifications.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Notifications;

[Authorize]
public class IndexModel : PageModel
{
    private readonly INotificationService _notificationService;

    public IndexModel(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public IReadOnlyList<NotificationDto> Notifications { get; set; } = [];
    public int UnreadCount { get; set; }

    public async Task OnGetAsync(CancellationToken ct)
    {
        var result = await _notificationService.GetPagedAsync(1, 100, null, null, null, ct);
        if (result.IsSuccess)
        {
            Notifications = result.Value!.Items;
        }

        var unreadResult = await _notificationService.GetUnreadCountAsync(ct);
        if (unreadResult.IsSuccess)
        {
            UnreadCount = unreadResult.Value;
        }
    }

    public async Task<IActionResult> OnPostMarkAsReadAsync(int id, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        await _notificationService.MarkAsReadAsync(id, userId, ct);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostMarkAllAsReadAsync(CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        await _notificationService.MarkAllAsReadAsync(userId, ct);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDismissAsync(int id, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        await _notificationService.DismissAsync(id, userId, ct);
        return RedirectToPage();
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(userIdClaim, out var userId) ? userId : 0;
    }
}
