using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetErp.Api.Pages.Admin.Insurance;

[Authorize]
public class EditModel : PageModel
{
    private readonly IInsuranceService _insuranceService;
    private readonly ILookupRepository _lookupRepository;

    public EditModel(
        IInsuranceService insuranceService,
        ILookupRepository lookupRepository)
    {
        _insuranceService = insuranceService;
        _lookupRepository = lookupRepository;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public int RecordId { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public string VehicleDisplay { get; set; } = string.Empty;

    public SelectList CompanyOptions { get; set; } = null!;
    public SelectList TypeOptions { get; set; } = null!;
    public SelectList StatusOptions { get; set; } = null!;

    public class InputModel
    {
        [Required(ErrorMessage = "Insurance company is required")]
        public int InsuranceCompanyId { get; set; }

        [Required(ErrorMessage = "Insurance type is required")]
        public int InsuranceTypeId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "Policy number is required")]
        [StringLength(100)]
        public string PolicyNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Premium is required")]
        [Range(0, double.MaxValue)]
        public decimal Premium { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? CoverageAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Deductible { get; set; }

        [StringLength(1000)]
        public string? CoverageDetails { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _insuranceService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Insurance record not found.";
            return RedirectToPage("Index");
        }

        var record = result.Value;
        RecordId = record.Id;
        PolicyNumber = record.PolicyNumber;
        VehicleDisplay = $"{record.VehiclePlateNumber} - {record.VehicleDescription}";

        Input = new InputModel
        {
            InsuranceCompanyId = record.InsuranceCompanyId,
            InsuranceTypeId = record.InsuranceTypeId,
            StatusId = record.StatusId,
            PolicyNumber = record.PolicyNumber,
            StartDate = record.StartDate,
            EndDate = record.EndDate,
            Premium = record.Premium,
            CoverageAmount = record.CoverageAmount,
            Deductible = record.Deductible,
            CoverageDetails = record.CoverageDetails,
            Notes = record.Notes
        };

        await LoadOptionsAsync(ct);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        RecordId = id;

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(ct);
            return Page();
        }

        var request = new UpdateInsuranceRequest(
            Input.InsuranceCompanyId,
            Input.InsuranceTypeId,
            Input.StatusId,
            Input.PolicyNumber,
            Input.StartDate,
            Input.EndDate,
            Input.Premium,
            Input.CoverageAmount,
            Input.Deductible,
            Input.CoverageDetails,
            Input.Notes);

        var result = await _insuranceService.UpdateAsync(id, request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to update insurance policy");
            await LoadOptionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Insurance policy #{id} updated successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadOptionsAsync(CancellationToken ct)
    {
        var companiesResult = await _insuranceService.GetAllActiveCompaniesAsync(ct);
        if (companiesResult.IsSuccess)
        {
            CompanyOptions = new SelectList(companiesResult.Value, "Id", "Name");
        }

        var types = await _lookupRepository.GetByTypeAsync(LookupTypes.InsuranceType, activeOnly: true, ct);
        TypeOptions = new SelectList(types, "Id", "Name");

        var statuses = await _lookupRepository.GetByTypeAsync(LookupTypes.InsuranceStatus, activeOnly: true, ct);
        StatusOptions = new SelectList(statuses, "Id", "Name");
    }
}
