using FleetErp.Application.Rentals.Dtos;
using FleetErp.Application.Rentals.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Rentals;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IRentalService _rentalService;

    public DeleteModel(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public RentalDto Rental { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _rentalService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Rental not found.";
            return RedirectToPage("Index");
        }

        Rental = result.Value;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        var result = await _rentalService.DeleteAsync(id, ct);
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete rental.";
            return RedirectToPage("Index");
        }

        TempData["SuccessMessage"] = "Rental deleted successfully.";
        return RedirectToPage("Index");
    }
}
