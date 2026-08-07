using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Application.Maintenance.Dtos;
using FleetErp.Application.Maintenance.Interfaces;
using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetErp.Api.Pages.Admin.Maintenance;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IMaintenanceService _maintenanceService;
    private readonly IVehicleService _vehicleService;
    private readonly ILookupRepository _lookupRepository;

    public CreateModel(
        IMaintenanceService maintenanceService,
        IVehicleService vehicleService,
        ILookupRepository lookupRepository)
    {
        _maintenanceService = maintenanceService;
        _vehicleService = vehicleService;
        _lookupRepository = lookupRepository;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public SelectList VehicleOptions { get; set; } = null!;
    public SelectList MaintenanceTypeOptions { get; set; } = null!;
    public SelectList StatusOptions { get; set; } = null!;

    public class InputModel
    {
        [Required(ErrorMessage = "Vehicle is required")]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Maintenance type is required")]
        [Display(Name = "Maintenance Type")]
        public int MaintenanceTypeId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Scheduled date is required")]
        [Display(Name = "Scheduled Date")]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; } = DateTime.Today;

        [Display(Name = "Odometer")]
        [Range(0, int.MaxValue)]
        public int? OdometerAtService { get; set; }

        [StringLength(200)]
        [Display(Name = "Service Provider")]
        public string? ServiceProvider { get; set; }

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

        var request = new CreateMaintenanceRequest(
            Input.VehicleId,
            Input.MaintenanceTypeId,
            Input.StatusId,
            Input.Description,
            Input.Cost,
            Input.ScheduledDate,
            Input.OdometerAtService,
            Input.ServiceProvider,
            Input.Notes);

        var result = await _maintenanceService.CreateAsync(request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to create maintenance record");
            await LoadOptionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Maintenance record #{result.Value!.Id} created successfully.";
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

        // Load maintenance types
        var types = await _lookupRepository.GetByTypeAsync(LookupTypes.MaintenanceType, activeOnly: true, ct);
        MaintenanceTypeOptions = new SelectList(types, "Id", "Name");

        // Load maintenance statuses
        var statuses = await _lookupRepository.GetByTypeAsync(LookupTypes.MaintenanceStatus, activeOnly: true, ct);
        StatusOptions = new SelectList(statuses, "Id", "Name");
    }
}
