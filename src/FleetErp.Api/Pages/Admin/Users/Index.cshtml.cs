using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Users;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IUserService _userService;

    public IndexModel(IUserService userService)
    {
        _userService = userService;
    }

    public IReadOnlyList<UserDto> Users { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        var result = await _userService.GetPagedAsync(1, 1000, null, ct);
        if (result.IsSuccess)
        {
            Users = result.Value!.Items;
        }
    }

    public async Task<IActionResult> OnPostActivateAsync(int id, CancellationToken ct)
    {
        var result = await _userService.ActivateAsync(id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to activate user.";
        }
        else
        {
            TempData["SuccessMessage"] = "User activated successfully.";
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeactivateAsync(int id, CancellationToken ct)
    {
        var result = await _userService.DeactivateAsync(id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to deactivate user.";
        }
        else
        {
            TempData["SuccessMessage"] = "User deactivated successfully.";
        }

        return RedirectToPage();
    }
}
