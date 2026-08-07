namespace FleetErp.Application.Vehicles.Dtos;

public record VehicleDocumentDto(
    int Id,
    int VehicleId,
    int DocumentTypeId,
    string DocumentTypeName,
    string FilePath,
    DateTime? ExpiresAt,
    bool IsExpired,
    DateTime CreatedAt
);

public record CreateVehicleDocumentRequest(
    int DocumentTypeId,
    string FilePath,
    DateTime? ExpiresAt
);
