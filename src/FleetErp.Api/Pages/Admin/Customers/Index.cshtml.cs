using FleetErp.Application.Customers.Dtos;
using FleetErp.Application.Customers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Customers;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ICustomerService _customerService;

    public IndexModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public IReadOnlyList<CustomerListDto> Customers { get; set; } = [];

    public async Task OnGetAsync(CancellationToken ct)
    {
        var result = await _customerService.GetPagedAsync(1, 1000, null, ct);
        if (result.IsSuccess)
        {
            Customers = result.Value!.Items;
        }
    }
}
