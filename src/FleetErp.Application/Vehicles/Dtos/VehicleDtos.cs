namespace FleetErp.Application.Vehicles.Dtos;

public record VehicleDto(
    int Id,
    string PlateNumber,
    string Make,
    string Model,
    int Year,
    string? Color,
    string? Vin,
    string? EngineNumber,
    string? ChassisNumber,
    int Odometer,
    decimal DailyRate,
    DateTime? PurchaseDate,
    decimal? PurchasePrice,
    string? Notes,
    int InvestorId,
    string InvestorName,
    int StatusId,
    string StatusName,
    int VehicleTypeId,
    string VehicleTypeName,
    int FuelTypeId,
    string FuelTypeName,
    int TransmissionTypeId,
    string TransmissionTypeName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int DocumentCount
);

public record VehicleListDto(
    int Id,
    string PlateNumber,
    string Make,
    string Model,
    int Year,
    string? Color,
    decimal DailyRate,
    string InvestorName,
    string StatusName,
    string VehicleTypeName,
    int DocumentCount
);

public record CreateVehicleRequest(
    string PlateNumber,
    string Make,
    string Model,
    int Year,
    string? Color,
    string? Vin,
    string? EngineNumber,
    string? ChassisNumber,
    int Odometer,
    decimal DailyRate,
    DateTime? PurchaseDate,
    decimal? PurchasePrice,
    string? Notes,
    int InvestorId,
    int StatusId,
    int VehicleTypeId,
    int FuelTypeId,
    int TransmissionTypeId
);

public record UpdateVehicleRequest(
    string PlateNumber,
    string Make,
    string Model,
    int Year,
    string? Color,
    string? Vin,
    string? EngineNumber,
    string? ChassisNumber,
    int Odometer,
    decimal DailyRate,
    DateTime? PurchaseDate,
    decimal? PurchasePrice,
    string? Notes,
    int InvestorId,
    int StatusId,
    int VehicleTypeId,
    int FuelTypeId,
    int TransmissionTypeId
);
