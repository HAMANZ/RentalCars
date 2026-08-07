using FleetErp.Application.Customers.Dtos;
using FleetErp.Application.Customers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Customers;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly ICustomerService _customerService;

    public DetailsModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public CustomerDto Customer { get; set; } = null!;
    public IReadOnlyList<CustomerDocumentDto> Documents { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _customerService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Customer not found.";
            return RedirectToPage("Index");
        }

        Customer = result.Value;

        var docsResult = await _customerService.GetDocumentsAsync(id, ct);
        if (docsResult.IsSuccess)
        {
            Documents = docsResult.Value!;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteDocumentAsync(int id, int documentId, CancellationToken ct)
    {
        var result = await _customerService.DeleteDocumentAsync(id, documentId, ct);
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
