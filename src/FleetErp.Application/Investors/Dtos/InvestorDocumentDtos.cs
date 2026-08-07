namespace FleetErp.Application.Investors.Dtos;

public record InvestorDocumentDto(
    int Id,
    int InvestorId,
    int DocumentTypeId,
    string DocumentTypeName,
    string FilePath,
    DateTime? ExpiresAt,
    bool IsExpired,
    DateTime CreatedAt
);

public record CreateInvestorDocumentRequest(
    int DocumentTypeId,
    string FilePath,
    DateTime? ExpiresAt
);
