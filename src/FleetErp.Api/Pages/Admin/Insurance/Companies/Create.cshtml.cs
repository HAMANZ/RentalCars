using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Insurance.Companies;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IInsuranceService _insuranceService;

    public CreateModel(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Phone]
        [StringLength(50)]
        public string? Phone { get; set; }

        [EmailAddress]
        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(200)]
        public string? ContactPerson { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new CreateInsuranceCompanyRequest(
            Input.Name,
            Input.Phone,
            Input.Email,
            Input.Address,
            Input.ContactPerson,
            Input.IsActive);

        var result = await _insuranceService.CreateCompanyAsync(request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to create insurance company");
            return Page();
        }

        TempData["SuccessMessage"] = $"Insurance company '{Input.Name}' created successfully.";
        return RedirectToPage("Index");
    }
}
