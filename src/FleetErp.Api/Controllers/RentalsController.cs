using FleetErp.Api.Common;
using FleetErp.Application.Rentals.Dtos;
using FleetErp.Application.Rentals.Interfaces;
using FleetErp.Infrastructure.Authorization;
using FleetErp.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Controllers;

public class RentalsController : ApiControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    /// <summary>
    /// Get paginated list of rentals
    /// </summary>
    [HttpGet]
    [HasPermission(Permissions.Rentals.View)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] int? customerId = null,
        [FromQuery] int? vehicleId = null,
        [FromQuery] int? statusId = null,
        CancellationToken ct = default)
    {
        var result = await _rentalService.GetPagedAsync(page, pageSize, search, customerId, vehicleId, statusId, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get rental by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [HasPermission(Permissions.Rentals.View)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _rentalService.GetByIdAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new rental
    /// </summary>
    [HttpPost]
    [HasPermission(Permissions.Rentals.Create)]
    public async Task<IActionResult> Create([FromBody] CreateRentalRequest request, CancellationToken ct)
    {
        var result = await _rentalService.CreateAsync(request, ct);
        return result.ToCreatedResult(this, nameof(GetById), new { id = result.Value?.Id });
    }

    /// <summary>
    /// Update an existing rental
    /// </summary>
    [HttpPut("{id:int}")]
    [HasPermission(Permissions.Rentals.Edit)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRentalRequest request, CancellationToken ct)
    {
        var result = await _rentalService.UpdateAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a rental (soft delete)
    /// </summary>
    [HttpDelete("{id:int}")]
    [HasPermission(Permissions.Rentals.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _rentalService.DeleteAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Complete a rental
    /// </summary>
    [HttpPost("{id:int}/complete")]
    [HasPermission(Permissions.Rentals.Complete)]
    public async Task<IActionResult> Complete(int id, [FromBody] CompleteRentalRequest request, CancellationToken ct)
    {
        var result = await _rentalService.CompleteAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Cancel a rental
    /// </summary>
    [HttpPost("{id:int}/cancel")]
    [HasPermission(Permissions.Rentals.Cancel)]
    public async Task<IActionResult> Cancel(int id, CancellationToken ct)
    {
        var result = await _rentalService.CancelAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get payments for a rental
    /// </summary>
    [HttpGet("{id:int}/payments")]
    [HasPermission(Permissions.Rentals.View)]
    public async Task<IActionResult> GetPayments(int id, CancellationToken ct)
    {
        var result = await _rentalService.GetPaymentsAsync(id, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Add a payment to a rental
    /// </summary>
    [HttpPost("{id:int}/payments")]
    [HasPermission(Permissions.Rentals.AddPayment)]
    public async Task<IActionResult> AddPayment(int id, [FromBody] CreateRentalPaymentRequest request, CancellationToken ct)
    {
        var result = await _rentalService.AddPaymentAsync(id, request, ct);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a payment from a rental
    /// </summary>
    [HttpDelete("{id:int}/payments/{paymentId:int}")]
    [HasPermission(Permissions.Rentals.AddPayment)]
    public async Task<IActionResult> DeletePayment(int id, int paymentId, CancellationToken ct)
    {
        var result = await _rentalService.DeletePaymentAsync(id, paymentId, ct);
        return result.ToActionResult(this);
    }
}
