using FleetErp.Application.Vehicles.Dtos;
using FluentValidation;

namespace FleetErp.Application.Vehicles.Validators;

public class CreateVehicleRequestValidator : AbstractValidator<CreateVehicleRequest>
{
    public CreateVehicleRequestValidator()
    {
        RuleFor(x => x.PlateNumber)
            .NotEmpty().WithMessage("Plate number is required")
            .MaximumLength(20).WithMessage("Plate number cannot exceed 20 characters");

        RuleFor(x => x.Make)
            .NotEmpty().WithMessage("Make is required")
            .MaximumLength(50).WithMessage("Make cannot exceed 50 characters");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required")
            .MaximumLength(50).WithMessage("Model cannot exceed 50 characters");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, 2100).WithMessage("Year must be between 1900 and 2100");

        RuleFor(x => x.Color)
            .MaximumLength(30).WithMessage("Color cannot exceed 30 characters");

        RuleFor(x => x.Vin)
            .MaximumLength(50).WithMessage("VIN cannot exceed 50 characters");

        RuleFor(x => x.EngineNumber)
            .MaximumLength(50).WithMessage("Engine number cannot exceed 50 characters");

        RuleFor(x => x.ChassisNumber)
            .MaximumLength(50).WithMessage("Chassis number cannot exceed 50 characters");

        RuleFor(x => x.Odometer)
            .GreaterThanOrEqualTo(0).WithMessage("Odometer cannot be negative");

        RuleFor(x => x.DailyRate)
            .GreaterThanOrEqualTo(0).WithMessage("Daily rate cannot be negative");

        RuleFor(x => x.PurchasePrice)
            .GreaterThanOrEqualTo(0).When(x => x.PurchasePrice.HasValue)
            .WithMessage("Purchase price cannot be negative");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");

        RuleFor(x => x.InvestorId)
            .GreaterThan(0).WithMessage("Investor is required");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");

        RuleFor(x => x.VehicleTypeId)
            .GreaterThan(0).WithMessage("Vehicle type is required");

        RuleFor(x => x.FuelTypeId)
            .GreaterThan(0).WithMessage("Fuel type is required");

        RuleFor(x => x.TransmissionTypeId)
            .GreaterThan(0).WithMessage("Transmission type is required");
    }
}

public class UpdateVehicleRequestValidator : AbstractValidator<UpdateVehicleRequest>
{
    public UpdateVehicleRequestValidator()
    {
        RuleFor(x => x.PlateNumber)
            .NotEmpty().WithMessage("Plate number is required")
            .MaximumLength(20).WithMessage("Plate number cannot exceed 20 characters");

        RuleFor(x => x.Make)
            .NotEmpty().WithMessage("Make is required")
            .MaximumLength(50).WithMessage("Make cannot exceed 50 characters");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required")
            .MaximumLength(50).WithMessage("Model cannot exceed 50 characters");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, 2100).WithMessage("Year must be between 1900 and 2100");

        RuleFor(x => x.Color)
            .MaximumLength(30).WithMessage("Color cannot exceed 30 characters");

        RuleFor(x => x.Vin)
            .MaximumLength(50).WithMessage("VIN cannot exceed 50 characters");

        RuleFor(x => x.EngineNumber)
            .MaximumLength(50).WithMessage("Engine number cannot exceed 50 characters");

        RuleFor(x => x.ChassisNumber)
            .MaximumLength(50).WithMessage("Chassis number cannot exceed 50 characters");

        RuleFor(x => x.Odometer)
            .GreaterThanOrEqualTo(0).WithMessage("Odometer cannot be negative");

        RuleFor(x => x.DailyRate)
            .GreaterThanOrEqualTo(0).WithMessage("Daily rate cannot be negative");

        RuleFor(x => x.PurchasePrice)
            .GreaterThanOrEqualTo(0).When(x => x.PurchasePrice.HasValue)
            .WithMessage("Purchase price cannot be negative");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");

        RuleFor(x => x.InvestorId)
            .GreaterThan(0).WithMessage("Investor is required");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");

        RuleFor(x => x.VehicleTypeId)
            .GreaterThan(0).WithMessage("Vehicle type is required");

        RuleFor(x => x.FuelTypeId)
            .GreaterThan(0).WithMessage("Fuel type is required");

        RuleFor(x => x.TransmissionTypeId)
            .GreaterThan(0).WithMessage("Transmission type is required");
    }
}

public class CreateVehicleDocumentRequestValidator : AbstractValidator<CreateVehicleDocumentRequest>
{
    public CreateVehicleDocumentRequestValidator()
    {
        RuleFor(x => x.DocumentTypeId)
            .GreaterThan(0).WithMessage("Document type is required");

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage("File path is required")
            .MaximumLength(500).WithMessage("File path cannot exceed 500 characters");
    }
}
