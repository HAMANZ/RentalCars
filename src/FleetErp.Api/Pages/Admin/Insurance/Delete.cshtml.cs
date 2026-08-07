using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Insurance;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IInsuranceService _insuranceService;

    public DeleteModel(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    public InsuranceRecordDto Record { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _insuranceService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Insurance record not found.";
            return RedirectToPage("Index");
        }

        Record = result.Value;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        var result = await _insuranceService.DeleteAsync(id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete insurance policy.";
            return RedirectToPage(new { id });
        }

        TempData["SuccessMessage"] = "Insurance policy deleted successfully.";
        return RedirectToPage("Index");
    }
}
