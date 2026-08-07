using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Customers.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Application.Rentals.Dtos;
using FleetErp.Application.Rentals.Interfaces;
using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetErp.Api.Pages.Admin.Rentals;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IRentalService _rentalService;
    private readonly IVehicleService _vehicleService;
    private readonly ICustomerService _customerService;
    private readonly ILookupRepository _lookupRepository;

    public CreateModel(
        IRentalService rentalService,
        IVehicleService vehicleService,
        ICustomerService customerService,
        ILookupRepository lookupRepository)
    {
        _rentalService = rentalService;
        _vehicleService = vehicleService;
        _customerService = customerService;
        _lookupRepository = lookupRepository;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public SelectList VehicleOptions { get; set; } = null!;
    public SelectList CustomerOptions { get; set; } = null!;
    public SelectList StatusOptions { get; set; } = null!;

    public class InputModel
    {
        [Required(ErrorMessage = "Vehicle is required")]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "Odometer start is required")]
        [Display(Name = "Odometer Start")]
        [Range(0, int.MaxValue)]
        public int OdometerStart { get; set; }

        [Required(ErrorMessage = "Daily rate is required")]
        [Display(Name = "Daily Rate")]
        [Range(0, double.MaxValue)]
        public decimal DailyRate { get; set; }

        [Display(Name = "Discount")]
        [Range(0, double.MaxValue)]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

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

        var request = new CreateRentalRequest(
            Input.VehicleId,
            Input.CustomerId,
            Input.StartDate,
            Input.EndDate,
            Input.OdometerStart,
            Input.DailyRate,
            Input.Discount,
            Input.StatusId,
            Input.Notes);

        var result = await _rentalService.CreateAsync(request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to create rental");
            await LoadOptionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Rental '{result.Value!.RentalNumber}' created successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadOptionsAsync(CancellationToken ct)
    {
        // Load available vehicles
        var vehiclesResult = await _vehicleService.GetPagedAsync(1, 1000, null, null, null, null, ct);
        if (vehiclesResult.IsSuccess)
        {
            var vehicleItems = vehiclesResult.Value!.Items
                .Select(v => new { v.Id, Display = $"{v.PlateNumber} - {v.Make} {v.Model} ({v.StatusName})" });
            VehicleOptions = new SelectList(vehicleItems, "Id", "Display");
        }

        // Load active customers
        var customersResult = await _customerService.GetPagedAsync(1, 1000, null, ct);
        if (customersResult.IsSuccess)
        {
            var customerItems = customersResult.Value!.Items
                .Select(c => new { c.Id, Display = $"{c.FullName} ({c.Phone ?? "No phone"})" });
            CustomerOptions = new SelectList(customerItems, "Id", "Display");
        }

        // Load rental statuses
        var statuses = await _lookupRepository.GetByTypeAsync(LookupTypes.RentalStatus, activeOnly: true, ct);
        StatusOptions = new SelectList(statuses, "Id", "Name");
    }
}
