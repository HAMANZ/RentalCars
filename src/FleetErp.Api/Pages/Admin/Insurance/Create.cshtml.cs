using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Insurance.Dtos;
using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetErp.Api.Pages.Admin.Insurance;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IInsuranceService _insuranceService;
    private readonly IVehicleService _vehicleService;
    private readonly ILookupRepository _lookupRepository;

    public CreateModel(
        IInsuranceService insuranceService,
        IVehicleService vehicleService,
        ILookupRepository lookupRepository)
    {
        _insuranceService = insuranceService;
        _vehicleService = vehicleService;
        _lookupRepository = lookupRepository;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public SelectList VehicleOptions { get; set; } = null!;
    public SelectList CompanyOptions { get; set; } = null!;
    public SelectList TypeOptions { get; set; } = null!;
    public SelectList StatusOptions { get; set; } = null!;

    public class InputModel
    {
        [Required(ErrorMessage = "Vehicle is required")]
        public int VehicleId { get; set; }

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
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Today.AddYears(1);

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

    public async Task OnGetAsync(CancellationToken ct)
    {
        await LoadOptionsAsync(ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(ct);
            return Page();
        }

        var request = new CreateInsuranceRequest(
            Input.VehicleId,
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

        var result = await _insuranceService.CreateAsync(request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to create insurance policy");
            await LoadOptionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Insurance policy #{result.Value!.Id} created successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadOptionsAsync(CancellationToken ct)
    {
        // Load vehicles
        var vehiclesResult = await _vehicleService.GetPagedAsync(1, 1000, null, null, null, null, ct);
        if (vehiclesResult.IsSuccess)
        {
            var vehicleItems = vehiclesResult.Value!.Items
                .Select(v => new { v.Id, Display = $"{v.PlateNumber} - {v.Make} {v.Model}" });
            VehicleOptions = new SelectList(vehicleItems, "Id", "Display");
        }

        // Load insurance companies
        var companiesResult = await _insuranceService.GetAllActiveCompaniesAsync(ct);
        if (companiesResult.IsSuccess)
        {
            CompanyOptions = new SelectList(companiesResult.Value, "Id", "Name");
        }

        // Load insurance types
        var types = await _lookupRepository.GetByTypeAsync(LookupTypes.InsuranceType, activeOnly: true, ct);
        TypeOptions = new SelectList(types, "Id", "Name");

        // Load insurance statuses
        var statuses = await _lookupRepository.GetByTypeAsync(LookupTypes.InsuranceStatus, activeOnly: true, ct);
        StatusOptions = new SelectList(statuses, "Id", "Name");
    }
}
