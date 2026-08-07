using FleetErp.Application.Common;
using FleetErp.Application.Rentals.Dtos;

namespace FleetErp.Application.Rentals.Interfaces;

public interface IRentalService
{
    Task<Result<RentalDto>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<PagedResult<RentalListDto>>> GetPagedAsync(int page, int pageSize, string? search, int? customerId, int? vehicleId, int? statusId, CancellationToken ct = default);
    Task<Result<IReadOnlyList<RentalListDto>>> GetActiveRentalsAsync(CancellationToken ct = default);
    Task<Result<RentalDto>> CreateAsync(CreateRentalRequest request, CancellationToken ct = default);
    Task<Result<RentalDto>> UpdateAsync(int id, UpdateRentalRequest request, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);

    // Rental lifecycle
    Task<Result<RentalDto>> CompleteAsync(int id, CompleteRentalRequest request, CancellationToken ct = default);
    Task<Result<RentalDto>> CancelAsync(int id, CancellationToken ct = default);

    // Payment operations
    Task<Result<IReadOnlyList<RentalPaymentDto>>> GetPaymentsAsync(int rentalId, CancellationToken ct = default);
    Task<Result<RentalPaymentDto>> AddPaymentAsync(int rentalId, CreateRentalPaymentRequest request, CancellationToken ct = default);
    Task<Result> DeletePaymentAsync(int rentalId, int paymentId, CancellationToken ct = default);
}
