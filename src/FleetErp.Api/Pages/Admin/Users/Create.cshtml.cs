using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Users;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public CreateModel(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

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

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "At least one role must be selected")]
        [Display(Name = "Roles")]
        public List<int> RoleIds { get; set; } = [];

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }

    public async Task OnGetAsync(CancellationToken ct)
    {
        await LoadRolesAsync(ct);
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

        var request = new CreateUserRequest(
            Input.FullName,
            Input.Email,
            Input.Password,
            Input.IsActive,
            Input.RoleIds);

        var result = await _userService.CreateAsync(request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to create user");
            await LoadRolesAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"User '{Input.FullName}' created successfully.";
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
