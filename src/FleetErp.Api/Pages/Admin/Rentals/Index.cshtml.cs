using FleetErp.Application.Rentals.Dtos;
using FleetErp.Application.Rentals.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Rentals;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IRentalService _rentalService;

    public IndexModel(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public IReadOnlyList<RentalListDto> Rentals { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        var result = await _rentalService.GetPagedAsync(1, 1000, null, null, null, null, ct);
        if (result.IsSuccess)
        {
            Rentals = result.Value!.Items;
        }
    }
}
