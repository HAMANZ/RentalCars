namespace FleetErp.Application.Rentals.Dtos;

public record RentalDto(
    int Id,
    string RentalNumber,
    int VehicleId,
    string VehiclePlateNumber,
    string VehicleDescription,
    int CustomerId,
    string CustomerName,
    DateTime StartDate,
    DateTime EndDate,
    DateTime? ActualReturnDate,
    int OdometerStart,
    int? OdometerEnd,
    decimal DailyRate,
    int TotalDays,
    decimal SubTotal,
    decimal Discount,
    decimal TotalAmount,
    decimal PaidAmount,
    decimal RemainingAmount,
    int StatusId,
    string StatusName,
    int PaymentStatusId,
    string PaymentStatusName,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int PaymentCount
);

public record RentalListDto(
    int Id,
    string RentalNumber,
    string VehiclePlateNumber,
    string VehicleDescription,
    string CustomerName,
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalAmount,
    decimal PaidAmount,
    string StatusName,
    string PaymentStatusName
);

public record CreateRentalRequest(
    int VehicleId,
    int CustomerId,
    DateTime StartDate,
    DateTime EndDate,
    int OdometerStart,
    decimal DailyRate,
    decimal Discount,
    int StatusId,
    string? Notes
);

public record UpdateRentalRequest(
    int VehicleId,
    int CustomerId,
    DateTime StartDate,
    DateTime EndDate,
    int OdometerStart,
    decimal DailyRate,
    decimal Discount,
    int StatusId,
    string? Notes
);

public record CompleteRentalRequest(
    DateTime ActualReturnDate,
    int OdometerEnd
);
