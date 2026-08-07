namespace FleetErp.Application.Investors.Dtos;

public record InvestorDto(
    int Id,
    string FullName,
    string? Phone,
    string? Email,
    string? NationalId,
    int StatusId,
    string StatusName,
    int? AccountId,
    DateTime CreatedAt,
    int DocumentCount
);

public record InvestorListDto(
    int Id,
    string FullName,
    string? Phone,
    string? Email,
    string StatusName,
    int DocumentCount
);

public record CreateInvestorRequest(
    string FullName,
    string? Phone,
    string? Email,
    string? NationalId,
    int StatusId
);

public record UpdateInvestorRequest(
    string FullName,
    string? Phone,
    string? Email,
    string? NationalId,
    int StatusId
);
