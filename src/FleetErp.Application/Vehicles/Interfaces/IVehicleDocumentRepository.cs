using FleetErp.Domain.Entities.Vehicles;

namespace FleetErp.Application.Vehicles.Interfaces;

public interface IVehicleDocumentRepository
{
    Task<VehicleDocument?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleDocument>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default);
    Task AddAsync(VehicleDocument document, CancellationToken ct = default);
    void Update(VehicleDocument document);
    void Delete(VehicleDocument document);
}
