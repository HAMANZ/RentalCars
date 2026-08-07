using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Insurance;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly IInsuranceService _insuranceService;

    public DetailsModel(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    public InsuranceRecordDto Record { get; set; } = null!;

    [BindProperty]
    public RenewInputModel RenewInput { get; set; } = new();

    public class RenewInputModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime NewStartDate { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        public DateTime NewEndDate { get; set; } = DateTime.Today.AddYears(1);

        [Required]
        [Range(0, double.MaxValue)]
        public decimal NewPremium { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? NewCoverageAmount { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _insuranceService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Insurance record not found.";
            return RedirectToPage("Index");
        }

        Record = result.Value;
        RenewInput.NewPremium = Record.Premium;
        RenewInput.NewCoverageAmount = Record.CoverageAmount;
        return Page();
    }

    public async Task<IActionResult> OnPostRenewAsync(int id, CancellationToken ct)
    {
        var request = new RenewInsuranceRequest(
            RenewInput.NewStartDate,
            RenewInput.NewEndDate,
            RenewInput.NewPremium,
            RenewInput.NewCoverageAmount,
            null,
            RenewInput.Notes);

        var result = await _insuranceService.RenewAsync(id, request, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to renew insurance policy.";
            return RedirectToPage(new { id });
        }

        TempData["SuccessMessage"] = "Insurance policy renewed successfully. A new policy has been created.";
        return RedirectToPage(new { id = result.Value!.Id });
    }

    public async Task<IActionResult> OnPostCancelAsync(int id, CancellationToken ct)
    {
        var result = await _insuranceService.CancelAsync(id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to cancel insurance policy.";
        }
        else
        {
            TempData["SuccessMessage"] = "Insurance policy cancelled successfully.";
        }

        return RedirectToPage(new { id });
    }
}
