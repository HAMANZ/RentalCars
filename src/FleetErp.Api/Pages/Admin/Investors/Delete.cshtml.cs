using FleetErp.Application.Investors.Dtos;
using FleetErp.Application.Investors.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetErp.Api.Pages.Admin.Investors;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IInvestorService _investorService;

    public DeleteModel(IInvestorService investorService)
    {
        _investorService = investorService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public InvestorDto? Investor { get; set; }

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        var result = await _investorService.GetByIdAsync(Id, ct);

        if (!result.IsSuccess || result.Value == null)
        {
            TempData["ErrorMessage"] = "Investor not found.";
            return RedirectToPage("Index");
        }

        Investor = result.Value;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        var investorResult = await _investorService.GetByIdAsync(Id, ct);
        var investorName = investorResult.Value?.FullName ?? "Investor";

        var result = await _investorService.DeleteAsync(Id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete investor.";
            return RedirectToPage(new { id = Id });
        }

        TempData["SuccessMessage"] = $"Investor '{investorName}' deleted successfully.";
        return RedirectToPage("Index");
    }
}
