using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Investors.Dtos;
using FleetErp.Application.Investors.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetErp.Api.Pages.Admin.Investors;

[Authorize]
public class EditModel : PageModel
{
    private readonly IInvestorService _investorService;
    private readonly ILookupRepository _lookupRepository;

    public EditModel(IInvestorService investorService, ILookupRepository lookupRepository)
    {
        _investorService = investorService;
        _lookupRepository = lookupRepository;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public SelectList StatusOptions { get; set; } = null!;

    public class InputModel
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(150, ErrorMessage = "Full name cannot exceed 150 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(50)]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(50)]
        [Display(Name = "National ID")]
        public string? NationalId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        var result = await _investorService.GetByIdAsync(Id, ct);

        if (!result.IsSuccess || result.Value == null)
        {
            TempData["ErrorMessage"] = "Investor not found.";
            return RedirectToPage("Index");
        }

        var investor = result.Value;
        Input = new InputModel
        {
            FullName = investor.FullName,
            Phone = investor.Phone,
            Email = investor.Email,
            NationalId = investor.NationalId,
            StatusId = investor.StatusId
        };

        await LoadStatusOptionsAsync(ct);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            await LoadStatusOptionsAsync(ct);
            return Page();
        }

        var request = new UpdateInvestorRequest(
            Input.FullName,
            Input.Phone,
            Input.Email,
            Input.NationalId,
            Input.StatusId);

        var result = await _investorService.UpdateAsync(Id, request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to update investor");
            await LoadStatusOptionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Investor '{Input.FullName}' updated successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadStatusOptionsAsync(CancellationToken ct)
    {
        var statuses = await _lookupRepository.GetByTypeAsync(LookupTypes.InvestorStatus, activeOnly: true, ct);
        StatusOptions = new SelectList(statuses, "Id", "Name");
    }
}
