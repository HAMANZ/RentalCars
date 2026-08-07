using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Insurance.Companies;

[Authorize]
public class EditModel : PageModel
{
    private readonly IInsuranceService _insuranceService;

    public EditModel(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;

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

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _insuranceService.GetCompanyByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Insurance company not found.";
            return RedirectToPage("Index");
        }

        var company = result.Value;
        CompanyId = company.Id;
        CompanyName = company.Name;

        Input = new InputModel
        {
            Name = company.Name,
            Phone = company.Phone,
            Email = company.Email,
            Address = company.Address,
            ContactPerson = company.ContactPerson,
            IsActive = company.IsActive
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        CompanyId = id;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new UpdateInsuranceCompanyRequest(
            Input.Name,
            Input.Phone,
            Input.Email,
            Input.Address,
            Input.ContactPerson,
            Input.IsActive);

        var result = await _insuranceService.UpdateCompanyAsync(id, request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to update insurance company");
            return Page();
        }

        TempData["SuccessMessage"] = $"Insurance company '{Input.Name}' updated successfully.";
        return RedirectToPage("Index");
    }
}
