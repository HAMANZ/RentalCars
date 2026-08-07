using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Maintenance.Dtos;
using FleetErp.Application.Maintenance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Maintenance;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly IMaintenanceService _maintenanceService;

    public DetailsModel(IMaintenanceService maintenanceService)
    {
        _maintenanceService = maintenanceService;
    }

    public MaintenanceRecordDto Record { get; set; } = null!;

    [BindProperty]
    public CompleteInputModel CompleteInput { get; set; } = new();

    public class CompleteInputModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime CompletedDate { get; set; } = DateTime.Today;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal FinalCost { get; set; }

        [Range(0, int.MaxValue)]
        public int? OdometerAtService { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _maintenanceService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Maintenance record not found.";
            return RedirectToPage("Index");
        }

        Record = result.Value;
        CompleteInput.FinalCost = Record.Cost;
        CompleteInput.OdometerAtService = Record.OdometerAtService;
        return Page();
    }

    public async Task<IActionResult> OnPostStartAsync(int id, CancellationToken ct)
    {
        var result = await _maintenanceService.StartAsync(id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to start maintenance.";
        }
        else
        {
            TempData["SuccessMessage"] = "Maintenance started successfully.";
        }

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostCompleteAsync(int id, CancellationToken ct)
    {
        var request = new CompleteMaintenanceRequest(
            CompleteInput.CompletedDate,
            CompleteInput.FinalCost,
            CompleteInput.OdometerAtService,
            CompleteInput.Notes);

        var result = await _maintenanceService.CompleteAsync(id, request, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to complete maintenance.";
        }
        else
        {
            TempData["SuccessMessage"] = "Maintenance completed successfully. Expense has been posted to the accounting system.";
        }

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostCancelAsync(int id, CancellationToken ct)
    {
        var result = await _maintenanceService.CancelAsync(id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to cancel maintenance.";
        }
        else
        {
            TempData["SuccessMessage"] = "Maintenance cancelled successfully.";
        }

        return RedirectToPage(new { id });
    }
}
