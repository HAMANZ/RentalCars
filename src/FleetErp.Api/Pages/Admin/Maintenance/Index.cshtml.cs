using FleetErp.Application.Maintenance.Dtos;
using FleetErp.Application.Maintenance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Maintenance;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IMaintenanceService _maintenanceService;

    public IndexModel(IMaintenanceService maintenanceService)
    {
        _maintenanceService = maintenanceService;
    }

    public IReadOnlyList<MaintenanceRecordListDto> Records { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        var result = await _maintenanceService.GetPagedAsync(1, 1000, null, null, null, null, ct);
        if (result.IsSuccess)
        {
            Records = result.Value!.Items;
        }
    }
}
