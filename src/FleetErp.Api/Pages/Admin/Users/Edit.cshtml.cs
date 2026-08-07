using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Users;

[Authorize]
public class EditModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public EditModel(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IReadOnlyList<RoleListDto> AvailableRoles { get; set; } = [];

    public class InputModel
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(200)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "At least one role must be selected")]
        [Display(Name = "Roles")]
        public List<int> RoleIds { get; set; } = [];
    }

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        var result = await _userService.GetByIdAsync(Id, ct);

        if (!result.IsSuccess || result.Value == null)
        {
            TempData["ErrorMessage"] = "User not found.";
            return RedirectToPage("Index");
        }

        var user = result.Value;
        await LoadRolesAsync(ct);

        // Get role IDs from role names
        var roleIds = AvailableRoles
            .Where(r => user.Roles.Contains(r.Name))
            .Select(r => r.Id)
            .ToList();

        Input = new InputModel
        {
            FullName = user.FullName,
            Email = user.Email,
            IsActive = user.IsActive,
            RoleIds = roleIds
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            await LoadRolesAsync(ct);
            return Page();
        }

        if (Input.RoleIds.Count == 0)
        {
            ModelState.AddModelError("Input.RoleIds", "At least one role must be selected");
            await LoadRolesAsync(ct);
            return Page();
        }

        var request = new UpdateUserRequest(
            Input.FullName,
            Input.Email,
            Input.IsActive,
            Input.RoleIds);

        var result = await _userService.UpdateAsync(Id, request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to update user");
            await LoadRolesAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"User '{Input.FullName}' updated successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadRolesAsync(CancellationToken ct)
    {
        var result = await _roleService.GetAllAsync(ct);
        if (result.IsSuccess)
        {
            AvailableRoles = result.Value!;
        }
    }
}
