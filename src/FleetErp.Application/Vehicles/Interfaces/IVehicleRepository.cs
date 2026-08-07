using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Vehicles;

namespace FleetErp.Application.Vehicles.Interfaces;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Vehicle?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default);
    Task<PagedResult<Vehicle>> GetPagedAsync(int page, int pageSize, string? search = null, int? investorId = null, int? statusId = null, CancellationToken ct = default);
    Task<bool> PlateNumberExistsAsync(string plateNumber, int? excludeVehicleId = null, CancellationToken ct = default);
    Task<bool> VinExistsAsync(string vin, int? excludeVehicleId = null, CancellationToken ct = default);
    Task<int> GetCountByInvestorIdAsync(int investorId, CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetAvailableAsync(CancellationToken ct = default);
    Task AddAsync(Vehicle vehicle, CancellationToken ct = default);
    void Update(Vehicle vehicle);
    void Delete(Vehicle vehicle);
}
