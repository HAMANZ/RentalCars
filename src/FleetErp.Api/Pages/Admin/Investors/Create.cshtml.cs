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
public class CreateModel : PageModel
{
    private readonly IInvestorService _investorService;
    private readonly ILookupRepository _lookupRepository;

    public CreateModel(IInvestorService investorService, ILookupRepository lookupRepository)
    {
        _investorService = investorService;
        _lookupRepository = lookupRepository;
    }

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

    public async Task OnGetAsync(CancellationToken ct)
    {
        await LoadStatusOptionsAsync(ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            await LoadStatusOptionsAsync(ct);
            return Page();
        }

        var request = new CreateInvestorRequest(
            Input.FullName,
            Input.Phone,
            Input.Email,
            Input.NationalId,
            Input.StatusId);

        var result = await _investorService.CreateAsync(request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to create investor");
            await LoadStatusOptionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Investor '{Input.FullName}' created successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadStatusOptionsAsync(CancellationToken ct)
    {
        var statuses = await _lookupRepository.GetByTypeAsync(LookupTypes.InvestorStatus, activeOnly: true, ct);
        StatusOptions = new SelectList(statuses, "Id", "Name");
    }
}
