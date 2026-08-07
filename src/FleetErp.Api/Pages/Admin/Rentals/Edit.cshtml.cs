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
public class EditModel : PageModel
{
    private readonly IRentalService _rentalService;
    private readonly IVehicleService _vehicleService;
    private readonly ICustomerService _customerService;
    private readonly ILookupRepository _lookupRepository;

    public EditModel(
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

    public int RentalId { get; set; }
    public string RentalNumber { get; set; } = string.Empty;

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
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

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

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _rentalService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Rental not found.";
            return RedirectToPage("Index");
        }

        var rental = result.Value;
        RentalId = rental.Id;
        RentalNumber = rental.RentalNumber;

        Input = new InputModel
        {
            VehicleId = rental.VehicleId,
            CustomerId = rental.CustomerId,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            OdometerStart = rental.OdometerStart,
            DailyRate = rental.DailyRate,
            Discount = rental.Discount,
            StatusId = rental.StatusId,
            Notes = rental.Notes
        };

        await LoadOptionsAsync(ct);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        RentalId = id;

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(ct);
            return Page();
        }

        var request = new UpdateRentalRequest(
            Input.VehicleId,
            Input.CustomerId,
            Input.StartDate,
            Input.EndDate,
            Input.OdometerStart,
            Input.DailyRate,
            Input.Discount,
            Input.StatusId,
            Input.Notes);

        var result = await _rentalService.UpdateAsync(id, request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to update rental");
            await LoadOptionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Rental '{result.Value!.RentalNumber}' updated successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadOptionsAsync(CancellationToken ct)
    {
        var vehiclesResult = await _vehicleService.GetPagedAsync(1, 1000, null, null, null, null, ct);
        if (vehiclesResult.IsSuccess)
        {
            var vehicleItems = vehiclesResult.Value!.Items
                .Select(v => new { v.Id, Display = $"{v.PlateNumber} - {v.Make} {v.Model} ({v.StatusName})" });
            VehicleOptions = new SelectList(vehicleItems, "Id", "Display");
        }

        var customersResult = await _customerService.GetPagedAsync(1, 1000, null, ct);
        if (customersResult.IsSuccess)
        {
            var customerItems = customersResult.Value!.Items
                .Select(c => new { c.Id, Display = $"{c.FullName} ({c.Phone ?? "No phone"})" });
            CustomerOptions = new SelectList(customerItems, "Id", "Display");
        }

        var statuses = await _lookupRepository.GetByTypeAsync(LookupTypes.RentalStatus, activeOnly: true, ct);
        StatusOptions = new SelectList(statuses, "Id", "Name");
    }
}
