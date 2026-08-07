namespace FleetErp.Application.Customers.Dtos;

public record CustomerDocumentDto(
    int Id,
    int CustomerId,
    int DocumentTypeId,
    string DocumentTypeName,
    string FilePath,
    DateTime? ExpiresAt,
    bool IsExpired,
    DateTime CreatedAt
);

public record CreateCustomerDocumentRequest(
    int DocumentTypeId,
    string FilePath,
    DateTime? ExpiresAt
);
