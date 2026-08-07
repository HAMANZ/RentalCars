using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Roles;

[Authorize]
public class EditModel : PageModel
{
    private readonly IRoleService _roleService;
    private readonly IPermissionService _permissionService;

    public EditModel(IRoleService roleService, IPermissionService permissionService)
    {
        _roleService = roleService;
        _permissionService = permissionService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

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

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        var result = await _roleService.GetByIdAsync(Id, ct);

        if (!result.IsSuccess || result.Value == null)
        {
            TempData["ErrorMessage"] = "Role not found.";
            return RedirectToPage("Index");
        }

        var role = result.Value;
        await LoadPermissionsAsync(ct);

        // Get current permission IDs for this role
        var rolePermissions = await _permissionService.GetByRoleIdAsync(Id, ct);
        var permissionIds = rolePermissions.IsSuccess
            ? rolePermissions.Value!.Select(p => p.Id).ToList()
            : [];

        Input = new InputModel
        {
            Name = role.Name,
            Description = role.Description,
            PermissionIds = permissionIds
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            await LoadPermissionsAsync(ct);
            return Page();
        }

        var request = new UpdateRoleRequest(
            Input.Name,
            Input.Description,
            Input.PermissionIds);

        var result = await _roleService.UpdateAsync(Id, request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to update role");
            await LoadPermissionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Role '{Input.Name}' updated successfully.";
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
