using FleetErp.Domain.Entities.Rentals;

namespace FleetErp.Application.Rentals.Interfaces;

public interface IRentalPaymentRepository
{
    Task<RentalPayment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<RentalPayment>> GetByRentalIdAsync(int rentalId, CancellationToken ct = default);
    Task AddAsync(RentalPayment payment, CancellationToken ct = default);
    void Delete(RentalPayment payment);
}
