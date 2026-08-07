using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Rentals;

namespace FleetErp.Application.Rentals.Interfaces;

public interface IRentalRepository
{
    Task<Rental?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Rental?> GetByIdWithPaymentsAsync(int id, CancellationToken ct = default);
    Task<PagedResult<Rental>> GetPagedAsync(int page, int pageSize, string? search, int? customerId, int? vehicleId, int? statusId, CancellationToken ct = default);
    Task<bool> RentalNumberExistsAsync(string rentalNumber, CancellationToken ct = default);
    Task<bool> VehicleHasActiveRentalAsync(int vehicleId, int? excludeRentalId = null, CancellationToken ct = default);
    Task<int> GetNextRentalSequenceAsync(DateTime date, CancellationToken ct = default);
    Task<IReadOnlyList<Rental>> GetActiveAsync(CancellationToken ct = default);
    Task AddAsync(Rental rental, CancellationToken ct = default);
    void Update(Rental rental);
    void Delete(Rental rental);
}
