namespace FleetErp.Application.Customers.Dtos;

public record CustomerDto(
    int Id,
    string FullName,
    string? Phone,
    string? Email,
    string? NationalId,
    string? DrivingLicenseNumber,
    DateTime? DrivingLicenseExpiry,
    string? Address,
    string? Notes,
    int StatusId,
    string StatusName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int DocumentCount,
    int RentalCount
);

public record CustomerListDto(
    int Id,
    string FullName,
    string? Phone,
    string? Email,
    string? NationalId,
    string StatusName,
    int RentalCount
);

public record CreateCustomerRequest(
    string FullName,
    string? Phone,
    string? Email,
    string? NationalId,
    string? DrivingLicenseNumber,
    DateTime? DrivingLicenseExpiry,
    string? Address,
    string? Notes,
    int StatusId
);

public record UpdateCustomerRequest(
    string FullName,
    string? Phone,
    string? Email,
    string? NationalId,
    string? DrivingLicenseNumber,
    DateTime? DrivingLicenseExpiry,
    string? Address,
    string? Notes,
    int StatusId
);
