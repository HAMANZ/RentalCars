using FleetErp.Application.Customers.Dtos;
using FleetErp.Application.Customers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Customers;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ICustomerService _customerService;

    public DeleteModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public CustomerDto Customer { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _customerService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Customer not found.";
            return RedirectToPage("Index");
        }

        Customer = result.Value;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        var result = await _customerService.DeleteAsync(id, ct);
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete customer.";
            return RedirectToPage("Index");
        }

        TempData["SuccessMessage"] = "Customer deleted successfully.";
        return RedirectToPage("Index");
    }
}
