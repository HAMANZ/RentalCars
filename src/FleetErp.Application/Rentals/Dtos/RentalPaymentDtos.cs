namespace FleetErp.Application.Rentals.Dtos;

public record RentalPaymentDto(
    int Id,
    int RentalId,
    decimal Amount,
    DateTime PaymentDate,
    int PaymentMethodId,
    string PaymentMethodName,
    string? TransactionReference,
    string? Notes,
    DateTime CreatedAt
);

public record CreateRentalPaymentRequest(
    decimal Amount,
    DateTime PaymentDate,
    int PaymentMethodId,
    string? TransactionReference,
    string? Notes
);
