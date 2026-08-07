using FleetErp.Application.Vehicles.Dtos;
using FleetErp.Application.Vehicles.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Vehicles;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly IVehicleService _vehicleService;

    public DetailsModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public VehicleDto Vehicle { get; set; } = null!;
    public IReadOnlyList<VehicleDocumentDto> Documents { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _vehicleService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Vehicle not found.";
            return RedirectToPage("Index");
        }

        Vehicle = result.Value;

        var docsResult = await _vehicleService.GetDocumentsAsync(id, ct);
        if (docsResult.IsSuccess)
        {
            Documents = docsResult.Value!;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteDocumentAsync(int id, int documentId, CancellationToken ct)
    {
        var result = await _vehicleService.DeleteDocumentAsync(id, documentId, ct);
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete document.";
        }
        else
        {
            TempData["SuccessMessage"] = "Document deleted successfully.";
        }

        return RedirectToPage(new { id });
    }
}
