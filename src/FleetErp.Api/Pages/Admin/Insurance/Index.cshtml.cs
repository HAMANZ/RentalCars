using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Insurance;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IInsuranceService _insuranceService;

    public IndexModel(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    public IReadOnlyList<InsuranceRecordListDto> Records { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        var result = await _insuranceService.GetPagedAsync(1, 1000, null, null, null, null, null, ct);
        if (result.IsSuccess)
        {
            Records = result.Value!.Items;
        }
    }
}
