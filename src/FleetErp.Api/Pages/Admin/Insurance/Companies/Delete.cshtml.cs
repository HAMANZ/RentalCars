using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Insurance.Companies;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IInsuranceService _insuranceService;

    public DeleteModel(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    public InsuranceCompanyDto Company { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _insuranceService.GetCompanyByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Insurance company not found.";
            return RedirectToPage("Index");
        }

        Company = result.Value;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        var result = await _insuranceService.DeleteCompanyAsync(id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete insurance company.";
            return RedirectToPage(new { id });
        }

        TempData["SuccessMessage"] = "Insurance company deleted successfully.";
        return RedirectToPage("Index");
    }
}
