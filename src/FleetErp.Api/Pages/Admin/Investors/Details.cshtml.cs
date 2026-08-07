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
public class DetailsModel : PageModel
{
    private readonly IInvestorService _investorService;
    private readonly ILookupRepository _lookupRepository;

    public DetailsModel(IInvestorService investorService, ILookupRepository lookupRepository)
    {
        _investorService = investorService;
        _lookupRepository = lookupRepository;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public InvestorDto? Investor { get; set; }

    public IReadOnlyList<InvestorDocumentDto> Documents { get; set; } = [];

    public SelectList DocumentTypeOptions { get; set; } = null!;

    [BindProperty]
    public DocumentInputModel NewDocument { get; set; } = new();

    public class DocumentInputModel
    {
        [Required]
        [Display(Name = "Document Type")]
        public int DocumentTypeId { get; set; }

        [Required]
        [Display(Name = "File Path")]
        public string FilePath { get; set; } = string.Empty;

        [Display(Name = "Expires At")]
        public DateTime? ExpiresAt { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        var result = await _investorService.GetByIdAsync(Id, ct);

        if (!result.IsSuccess || result.Value == null)
        {
            TempData["ErrorMessage"] = "Investor not found.";
            return RedirectToPage("Index");
        }

        Investor = result.Value;

        var docsResult = await _investorService.GetDocumentsAsync(Id, ct);
        if (docsResult.IsSuccess)
        {
            Documents = docsResult.Value!;
        }

        await LoadDocumentTypesAsync(ct);
        return Page();
    }

    public async Task<IActionResult> OnPostAddDocumentAsync(CancellationToken ct)
    {
        var request = new CreateInvestorDocumentRequest(
            NewDocument.DocumentTypeId,
            NewDocument.FilePath,
            NewDocument.ExpiresAt);

        var result = await _investorService.AddDocumentAsync(Id, request, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to add document.";
        }
        else
        {
            TempData["SuccessMessage"] = "Document added successfully.";
        }

        return RedirectToPage(new { id = Id });
    }

    public async Task<IActionResult> OnPostDeleteDocumentAsync(int documentId, CancellationToken ct)
    {
        var result = await _investorService.DeleteDocumentAsync(Id, documentId, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete document.";
        }
        else
        {
            TempData["SuccessMessage"] = "Document deleted successfully.";
        }

        return RedirectToPage(new { id = Id });
    }

    private async Task LoadDocumentTypesAsync(CancellationToken ct)
    {
        var types = await _lookupRepository.GetByTypeAsync(LookupTypes.DocumentType, activeOnly: true, ct);
        DocumentTypeOptions = new SelectList(types, "Id", "Name");
    }
}
