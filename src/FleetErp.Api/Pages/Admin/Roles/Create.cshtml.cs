using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Roles;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IRoleService _roleService;
    private readonly IPermissionService _permissionService;

    public CreateModel(IRoleService roleService, IPermissionService permissionService)
    {
        _roleService = roleService;
        _permissionService = permissionService;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IReadOnlyList<PermissionGroupDto> GroupedPermissions { get; set; } = [];

    public class InputModel
    {
        [Required(ErrorMessage = "Role name is required")]
        [StringLength(100)]
        [Display(Name = "Role Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Permissions")]
        public List<int> PermissionIds { get; set; } = [];
    }

    public async Task OnGetAsync(CancellationToken ct)
    {
        await LoadPermissionsAsync(ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            await LoadPermissionsAsync(ct);
            return Page();
        }

        var request = new CreateRoleRequest(
            Input.Name,
            Input.Description,
            Input.PermissionIds);

        var result = await _roleService.CreateAsync(request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to create role");
            await LoadPermissionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Role '{Input.Name}' created successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadPermissionsAsync(CancellationToken ct)
    {
        var result = await _permissionService.GetGroupedAsync(ct);
        if (result.IsSuccess)
        {
            GroupedPermissions = result.Value!;
        }
    }
}
