using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Customers.Dtos;
using FleetErp.Application.Customers.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetErp.Api.Pages.Admin.Customers;

[Authorize]
public class EditModel : PageModel
{
    private readonly ICustomerService _customerService;
    private readonly ILookupRepository _lookupRepository;

    public EditModel(
        ICustomerService customerService,
        ILookupRepository lookupRepository)
    {
        _customerService = customerService;
        _lookupRepository = lookupRepository;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;

    public SelectList StatusOptions { get; set; } = null!;

    public class InputModel
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [StringLength(50)]
        [Display(Name = "National ID")]
        public string? NationalId { get; set; }

        [StringLength(50)]
        [Display(Name = "Driving License Number")]
        public string? DrivingLicenseNumber { get; set; }

        [Display(Name = "License Expiry")]
        [DataType(DataType.Date)]
        public DateTime? DrivingLicenseExpiry { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var result = await _customerService.GetByIdAsync(id, ct);
        if (!result.IsSuccess || result.Value is null)
        {
            TempData["ErrorMessage"] = "Customer not found.";
            return RedirectToPage("Index");
        }

        var customer = result.Value;
        CustomerId = customer.Id;
        CustomerName = customer.FullName;

        Input = new InputModel
        {
            FullName = customer.FullName,
            Phone = customer.Phone,
            Email = customer.Email,
            NationalId = customer.NationalId,
            DrivingLicenseNumber = customer.DrivingLicenseNumber,
            DrivingLicenseExpiry = customer.DrivingLicenseExpiry,
            Address = customer.Address,
            Notes = customer.Notes,
            StatusId = customer.StatusId
        };

        await LoadOptionsAsync(ct);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        CustomerId = id;

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(ct);
            return Page();
        }

        var request = new UpdateCustomerRequest(
            Input.FullName,
            Input.Phone,
            Input.Email,
            Input.NationalId,
            Input.DrivingLicenseNumber,
            Input.DrivingLicenseExpiry,
            Input.Address,
            Input.Notes,
            Input.StatusId);

        var result = await _customerService.UpdateAsync(id, request, ct);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Failed to update customer");
            await LoadOptionsAsync(ct);
            return Page();
        }

        TempData["SuccessMessage"] = $"Customer '{Input.FullName}' updated successfully.";
        return RedirectToPage("Index");
    }

    private async Task LoadOptionsAsync(CancellationToken ct)
    {
        var statuses = await _lookupRepository.GetByTypeAsync(LookupTypes.CustomerStatus, activeOnly: true, ct);
        StatusOptions = new SelectList(statuses, "Id", "Name");
    }
}
