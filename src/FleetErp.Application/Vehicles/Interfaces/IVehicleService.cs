using FleetErp.Application.Common;
using FleetErp.Application.Vehicles.Dtos;

namespace FleetErp.Application.Vehicles.Interfaces;

public interface IVehicleService
{
    Task<Result<VehicleDto>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<PagedResult<VehicleListDto>>> GetPagedAsync(int page, int pageSize, string? search = null, int? investorId = null, int? statusId = null, int? vehicleTypeId = null, CancellationToken ct = default);
    Task<Result<IReadOnlyList<VehicleListDto>>> GetAvailableAsync(CancellationToken ct = default);
    Task<Result<VehicleDto>> CreateAsync(CreateVehicleRequest request, CancellationToken ct = default);
    Task<Result<VehicleDto>> UpdateAsync(int id, UpdateVehicleRequest request, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);

    // Document operations
    Task<Result<IReadOnlyList<VehicleDocumentDto>>> GetDocumentsAsync(int vehicleId, CancellationToken ct = default);
    Task<Result<VehicleDocumentDto>> AddDocumentAsync(int vehicleId, CreateVehicleDocumentRequest request, CancellationToken ct = default);
    Task<Result> DeleteDocumentAsync(int vehicleId, int documentId, CancellationToken ct = default);
}
