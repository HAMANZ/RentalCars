using FleetErp.Application.Rentals.Dtos;
using FluentValidation;

namespace FleetErp.Application.Rentals.Validators;

public class CreateRentalRequestValidator : AbstractValidator<CreateRentalRequest>
{
    public CreateRentalRequestValidator()
    {
        RuleFor(x => x.VehicleId)
            .GreaterThan(0).WithMessage("Vehicle is required");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer is required");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be on or after start date");

        RuleFor(x => x.OdometerStart)
            .GreaterThanOrEqualTo(0).WithMessage("Odometer start cannot be negative");

        RuleFor(x => x.DailyRate)
            .GreaterThan(0).WithMessage("Daily rate must be greater than zero");

        RuleFor(x => x.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");
    }
}

public class UpdateRentalRequestValidator : AbstractValidator<UpdateRentalRequest>
{
    public UpdateRentalRequestValidator()
    {
        RuleFor(x => x.VehicleId)
            .GreaterThan(0).WithMessage("Vehicle is required");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer is required");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be on or after start date");

        RuleFor(x => x.OdometerStart)
            .GreaterThanOrEqualTo(0).WithMessage("Odometer start cannot be negative");

        RuleFor(x => x.DailyRate)
            .GreaterThan(0).WithMessage("Daily rate must be greater than zero");

        RuleFor(x => x.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");
    }
}

public class CompleteRentalRequestValidator : AbstractValidator<CompleteRentalRequest>
{
    public CompleteRentalRequestValidator()
    {
        RuleFor(x => x.ActualReturnDate)
            .NotEmpty().WithMessage("Return date is required");

        RuleFor(x => x.OdometerEnd)
            .GreaterThanOrEqualTo(0).WithMessage("Odometer end cannot be negative");
    }
}

public class CreateRentalPaymentRequestValidator : AbstractValidator<CreateRentalPaymentRequest>
{
    public CreateRentalPaymentRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero");

        RuleFor(x => x.PaymentDate)
            .NotEmpty().WithMessage("Payment date is required");

        RuleFor(x => x.PaymentMethodId)
            .GreaterThan(0).WithMessage("Payment method is required");

        RuleFor(x => x.TransactionReference)
            .MaximumLength(100).WithMessage("Transaction reference cannot exceed 100 characters");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters");
    }
}
