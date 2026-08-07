using FleetErp.Application.Maintenance.Dtos;
using FluentValidation;

namespace FleetErp.Application.Maintenance.Validators;

public class CreateMaintenanceRequestValidator : AbstractValidator<CreateMaintenanceRequest>
{
    public CreateMaintenanceRequestValidator()
    {
        RuleFor(x => x.VehicleId)
            .GreaterThan(0).WithMessage("Vehicle is required");

        RuleFor(x => x.MaintenanceTypeId)
            .GreaterThan(0).WithMessage("Maintenance type is required");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");

        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).WithMessage("Cost cannot be negative");

        RuleFor(x => x.ScheduledDate)
            .NotEmpty().WithMessage("Scheduled date is required");

        RuleFor(x => x.OdometerAtService)
            .GreaterThanOrEqualTo(0).When(x => x.OdometerAtService.HasValue)
            .WithMessage("Odometer cannot be negative");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.ServiceProvider)
            .MaximumLength(200).WithMessage("Service provider cannot exceed 200 characters");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");
    }
}

public class UpdateMaintenanceRequestValidator : AbstractValidator<UpdateMaintenanceRequest>
{
    public UpdateMaintenanceRequestValidator()
    {
        RuleFor(x => x.MaintenanceTypeId)
            .GreaterThan(0).WithMessage("Maintenance type is required");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");

        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).WithMessage("Cost cannot be negative");

        RuleFor(x => x.ScheduledDate)
            .NotEmpty().WithMessage("Scheduled date is required");

        RuleFor(x => x.OdometerAtService)
            .GreaterThanOrEqualTo(0).When(x => x.OdometerAtService.HasValue)
            .WithMessage("Odometer cannot be negative");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.ServiceProvider)
            .MaximumLength(200).WithMessage("Service provider cannot exceed 200 characters");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");
    }
}

public class CompleteMaintenanceRequestValidator : AbstractValidator<CompleteMaintenanceRequest>
{
    public CompleteMaintenanceRequestValidator()
    {
        RuleFor(x => x.CompletedDate)
            .NotEmpty().WithMessage("Completed date is required");

        RuleFor(x => x.FinalCost)
            .GreaterThanOrEqualTo(0).WithMessage("Final cost cannot be negative");

        RuleFor(x => x.OdometerAtService)
            .GreaterThanOrEqualTo(0).When(x => x.OdometerAtService.HasValue)
            .WithMessage("Odometer cannot be negative");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");
    }
}
