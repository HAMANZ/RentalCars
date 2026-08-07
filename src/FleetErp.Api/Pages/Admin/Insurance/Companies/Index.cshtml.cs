using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Insurance.Companies;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IInsuranceService _insuranceService;

    public IndexModel(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    public IReadOnlyList<InsuranceCompanyDto> Companies { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        var result = await _insuranceService.GetAllCompaniesAsync(ct);
        if (result.IsSuccess)
        {
            Companies = result.Value!;
        }
    }
}
