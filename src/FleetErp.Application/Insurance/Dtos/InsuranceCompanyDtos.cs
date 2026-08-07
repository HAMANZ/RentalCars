namespace FleetErp.Application.Insurance.Dtos;

public record InsuranceCompanyDto(
    int Id,
    string Name,
    string? Phone,
    string? Email,
    string? Address,
    string? ContactPerson,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateInsuranceCompanyRequest(
    string Name,
    string? Phone,
    string? Email,
    string? Address,
    string? ContactPerson,
    bool IsActive = true
);

public record UpdateInsuranceCompanyRequest(
    string Name,
    string? Phone,
    string? Email,
    string? Address,
    string? ContactPerson,
    bool IsActive
);
