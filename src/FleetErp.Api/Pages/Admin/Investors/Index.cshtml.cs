using FleetErp.Application.Investors.Dtos;
using FleetErp.Application.Investors.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Investors;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IInvestorService _investorService;

    public IndexModel(IInvestorService investorService)
    {
        _investorService = investorService;
    }

    public IReadOnlyList<InvestorListDto> Investors { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        // Get all investors (large page size to get all for DataTables client-side processing)
        var result = await _investorService.GetPagedAsync(1, 1000, null, ct);
        if (result.IsSuccess)
        {
            Investors = result.Value!.Items;
        }
    }
}
