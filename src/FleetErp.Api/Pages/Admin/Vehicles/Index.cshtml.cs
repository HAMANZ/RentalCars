using FleetErp.Application.Vehicles.Dtos;
using FleetErp.Application.Vehicles.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Vehicles;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IVehicleService _vehicleService;

    public IndexModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public IReadOnlyList<VehicleListDto> Vehicles { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        // Get all vehicles (large page size to get all for DataTables client-side processing)
        var result = await _vehicleService.GetPagedAsync(1, 1000, null, null, null, null, ct);
        if (result.IsSuccess)
        {
            Vehicles = result.Value!.Items;
        }
    }
}
