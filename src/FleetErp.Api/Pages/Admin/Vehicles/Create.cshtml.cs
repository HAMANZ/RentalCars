using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Investors.Dtos;
using FleetErp.Application.Investors.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Application.Vehicles.Dtos;
using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetErp.Api.Pages.Admin.Vehicles;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IVehicleService _vehicleService;
    private readonly IInvestorService _investorService;
    private readonly ILookupRepository _lookupRepository;

    public CreateModel(
        IVehicleService vehicleService,
        IInvestorService investorService,
        ILookupRepository lookupRepository)
    {
        _vehicleService = vehicleService;
        _investorService = investorService;
        _lookupRepository = lookupRepository;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public SelectList InvestorOptions { get; set; } = null!;
    public SelectList StatusOptions { get; set; } = null!;
    public SelectList VehicleTypeOptions { get; set; } = null!;
    public SelectList FuelTypeOptions { get; set; } = null!;
    public SelectList TransmissionTypeOptions { get; set; } = null!;

    public class InputModel
    {
        [Required(ErrorMessage = "Plate number is required")]
        [StringLength(20, ErrorMessage = "Plate number cannot exceed 20 characters")]
        [Display(Name = "Plate Number")]
        public string PlateNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Make is required")]
        [StringLength(50, ErrorMessage = "Make cannot exceed 50 characters")]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
        public int Year { get; set; } = DateTime.Now.Year;

        [StringLength(30)]
        public string? Color { get; set; }

        [StringLength(50)]
        [Display(Name = "VIN")]
        public string? Vin { get; set; }

        [StringLength(50)]
        [Display(Name = "Engine Number")]
        public string? EngineNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Chassis Number")]
        public string? ChassisNumber { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Odometer cannot be negative")]
        public int Odometer { get; set; }

        [Required(ErrorMessage = "Daily rate is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Daily rate cannot be negative")]
        [Display(Name = "Daily Rate")]
        public decimal DailyRate { get; set; }

        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Purchase price cannot be negative")]
        [Display(Name = "Purchase Price")]
        public decimal? PurchasePrice { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Investor is required")]
        [Display(Name = "Investor (Owner)")]
        public int InvestorId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "Vehicle type is required")]
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; set; }

        [Required(ErrorMessage = "Fuel type is required")]
        [Display(Name = "Fuel Type")]
        public int FuelTypeId { get; set; }

        [Required(ErrorMessage = "Transmission type is required")]
        [Display(Name = "Transmission")]
        public int TransmissionTypeId { get; set; }
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

        var request = new CreateVehicleRequest(
            Input.PlateNumber,
            Input.Make,
            Input.Model,
            Input.Year,
            Input.Color,
            Input.Vin,
            Input.EngineNumber,
            Input.ChassisNumber,
            Input.Odometer,
            Input.DailyRate,
            Input.PurchaseDate,
            Input.PurchasePrice,
            Input.Notes,
            Input.InvestorId,
            Input.StatusId,
            Input.VehicleTypeId,
            Input.FuelTypeId,
            Input.TransmissionTypeId);

        var result = await _vehicleService.CreateAsync(request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to create vehicle");
            await LoadOptionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Vehicle '{Input.PlateNumber}' created successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadOptionsAsync(CancellationToken ct)
    {
        // Load investors
        var investorsResult = await _investorService.GetPagedAsync(1, 1000, null, ct);
        if (investorsResult.IsSuccess)
        {
            InvestorOptions = new SelectList(investorsResult.Value!.Items, "Id", "FullName");
        }

        // Load lookups
        var statuses = await _lookupRepository.GetByTypeAsync(LookupTypes.VehicleStatus, activeOnly: true, ct);
        StatusOptions = new SelectList(statuses, "Id", "Name");

        var vehicleTypes = await _lookupRepository.GetByTypeAsync(LookupTypes.VehicleType, activeOnly: true, ct);
        VehicleTypeOptions = new SelectList(vehicleTypes, "Id", "Name");

        var fuelTypes = await _lookupRepository.GetByTypeAsync(LookupTypes.FuelType, activeOnly: true, ct);
        FuelTypeOptions = new SelectList(fuelTypes, "Id", "Name");

        var transmissionTypes = await _lookupRepository.GetByTypeAsync(LookupTypes.TransmissionType, activeOnly: true, ct);
        TransmissionTypeOptions = new SelectList(transmissionTypes, "Id", "Name");
    }
}
