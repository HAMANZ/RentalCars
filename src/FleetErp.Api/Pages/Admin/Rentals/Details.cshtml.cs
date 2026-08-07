using System.ComponentModel.DataAnnotations;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Application.Rentals.Dtos;
using FleetErp.Application.Rentals.Interfaces;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetErp.Api.Pages.Admin.Rentals;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly IRentalService _rentalService;
    private readonly ILookupRepository _lookupRepository;

    public DetailsModel(IRentalService rentalService, ILookupRepository lookupRepository)
    {
        _rentalService = rentalService;
        _lookupRepository = lookupRepository;
    }

    public RentalDto Rental { get; set; } = null!;
    public IReadOnlyList<RentalPaymentDto> Payments { get; set; } = [];
    public SelectList PaymentMethodOptions { get; set; } = null!;

    [BindProperty]
    public CompleteInputModel CompleteInput { get; set; } = new();

    [BindProperty]
    public PaymentInputModel PaymentInput { get; set; } = new();

    public class CompleteInputModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime ActualReturnDate { get; set; } = DateTime.Today;

        [Required]
        [Range(0, int.MaxValue)]
        public int OdometerEnd { get; set; }
    }

    public class PaymentInputModel
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; } = DateTime.Today;

        [Required]
        public int PaymentMethodId { get; set; }

        [StringLength(100)]
        public string? TransactionReference { get; set; }

        [StringLength(500)]
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

        Rental = result.Value;

        var paymentsResult = await _rentalService.GetPaymentsAsync(id, ct);
        if (paymentsResult.IsSuccess)
        {
            Payments = paymentsResult.Value!;
        }

        await LoadPaymentMethodsAsync(ct);
        return Page();
    }

    public async Task<IActionResult> OnPostCompleteAsync(int id, CancellationToken ct)
    {
        var request = new CompleteRentalRequest(CompleteInput.ActualReturnDate, CompleteInput.OdometerEnd);
        var result = await _rentalService.CompleteAsync(id, request, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to complete rental.";
        }
        else
        {
            TempData["SuccessMessage"] = "Rental completed successfully.";
        }

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostCancelAsync(int id, CancellationToken ct)
    {
        var result = await _rentalService.CancelAsync(id, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to cancel rental.";
        }
        else
        {
            TempData["SuccessMessage"] = "Rental cancelled successfully.";
        }

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostAddPaymentAsync(int id, CancellationToken ct)
    {
        var request = new CreateRentalPaymentRequest(
            PaymentInput.Amount,
            PaymentInput.PaymentDate,
            PaymentInput.PaymentMethodId,
            PaymentInput.TransactionReference,
            PaymentInput.Notes);

        var result = await _rentalService.AddPaymentAsync(id, request, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to add payment.";
        }
        else
        {
            TempData["SuccessMessage"] = "Payment added successfully.";
        }

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostDeletePaymentAsync(int id, int paymentId, CancellationToken ct)
    {
        var result = await _rentalService.DeletePaymentAsync(id, paymentId, ct);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error ?? "Failed to delete payment.";
        }
        else
        {
            TempData["SuccessMessage"] = "Payment deleted successfully.";
        }

        return RedirectToPage(new { id });
    }

    private async Task LoadPaymentMethodsAsync(CancellationToken ct)
    {
        var methods = await _lookupRepository.GetByTypeAsync(LookupTypes.PaymentMethod, activeOnly: true, ct);
        PaymentMethodOptions = new SelectList(methods, "Id", "Name");
    }
}
