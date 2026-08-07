using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Roles;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IRoleService _roleService;

    public IndexModel(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public IReadOnlyList<RoleListDto> Roles { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        var result = await _roleService.GetAllAsync(ct);
        if (result.IsSuccess)
        {
            Roles = result.Value!;
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken ct)
    {
        var result = await _roleService.DeleteAsync(id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete role.";
        }
        else
        {
            TempData["SuccessMessage"] = "Role deleted successfully.";
        }

        return RedirectToPage();
    }
}
