using FleetErp.Application.Vehicles.Dtos;
using FleetErp.Application.Vehicles.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Vehicles;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IVehicleService _vehicleService;

    public DeleteModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public VehicleDto Vehicle { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _vehicleService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Vehicle not found.";
            return RedirectToPage("Index");
        }

        Vehicle = result.Value;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        var result = await _vehicleService.DeleteAsync(id, ct);
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete vehicle.";
            return RedirectToPage("Index");
        }

        TempData["SuccessMessage"] = "Vehicle deleted successfully.";
        return RedirectToPage("Index");
    }
}
