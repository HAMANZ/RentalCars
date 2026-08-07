using FleetErp.Application.Maintenance.Dtos;
using FleetErp.Application.Maintenance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Maintenance;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IMaintenanceService _maintenanceService;

    public DeleteModel(IMaintenanceService maintenanceService)
    {
        _maintenanceService = maintenanceService;
    }

    public MaintenanceRecordDto Record { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _maintenanceService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Maintenance record not found.";
            return RedirectToPage("Index");
        }

        Record = result.Value;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        var result = await _maintenanceService.DeleteAsync(id, ct);
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete maintenance record.";
            return RedirectToPage("Index");
        }

        TempData["SuccessMessage"] = "Maintenance record deleted successfully.";
        return RedirectToPage("Index");
    }
}
