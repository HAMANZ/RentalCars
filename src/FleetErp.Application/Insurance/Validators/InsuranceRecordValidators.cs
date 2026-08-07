using FleetErp.Application.Insurance.Dtos;
using FluentValidation;

namespace FleetErp.Application.Insurance.Validators;

public class CreateInsuranceRequestValidator : AbstractValidator<CreateInsuranceRequest>
{
    public CreateInsuranceRequestValidator()
    {
        RuleFor(x => x.VehicleId)
            .GreaterThan(0).WithMessage("Vehicle is required");

        RuleFor(x => x.InsuranceCompanyId)
            .GreaterThan(0).WithMessage("Insurance company is required");

        RuleFor(x => x.InsuranceTypeId)
            .GreaterThan(0).WithMessage("Insurance type is required");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");

        RuleFor(x => x.PolicyNumber)
            .NotEmpty().WithMessage("Policy number is required")
            .MaximumLength(100).WithMessage("Policy number cannot exceed 100 characters");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date");

        RuleFor(x => x.Premium)
            .GreaterThanOrEqualTo(0).WithMessage("Premium must be zero or positive");

        RuleFor(x => x.CoverageAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Coverage amount must be zero or positive")
            .When(x => x.CoverageAmount.HasValue);

        RuleFor(x => x.Deductible)
            .GreaterThanOrEqualTo(0).WithMessage("Deductible must be zero or positive")
            .When(x => x.Deductible.HasValue);

        RuleFor(x => x.CoverageDetails)
            .MaximumLength(1000).WithMessage("Coverage details cannot exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.CoverageDetails));

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Notes));
    }
}

public class UpdateInsuranceRequestValidator : AbstractValidator<UpdateInsuranceRequest>
{
    public UpdateInsuranceRequestValidator()
    {
        RuleFor(x => x.InsuranceCompanyId)
            .GreaterThan(0).WithMessage("Insurance company is required");

        RuleFor(x => x.InsuranceTypeId)
            .GreaterThan(0).WithMessage("Insurance type is required");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");

        RuleFor(x => x.PolicyNumber)
            .NotEmpty().WithMessage("Policy number is required")
            .MaximumLength(100).WithMessage("Policy number cannot exceed 100 characters");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date");

        RuleFor(x => x.Premium)
            .GreaterThanOrEqualTo(0).WithMessage("Premium must be zero or positive");

        RuleFor(x => x.CoverageAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Coverage amount must be zero or positive")
            .When(x => x.CoverageAmount.HasValue);

        RuleFor(x => x.Deductible)
            .GreaterThanOrEqualTo(0).WithMessage("Deductible must be zero or positive")
            .When(x => x.Deductible.HasValue);

        RuleFor(x => x.CoverageDetails)
            .MaximumLength(1000).WithMessage("Coverage details cannot exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.CoverageDetails));

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Notes));
    }
}

public class RenewInsuranceRequestValidator : AbstractValidator<RenewInsuranceRequest>
{
    public RenewInsuranceRequestValidator()
    {
        RuleFor(x => x.NewStartDate)
            .NotEmpty().WithMessage("New start date is required");

        RuleFor(x => x.NewEndDate)
            .NotEmpty().WithMessage("New end date is required")
            .GreaterThan(x => x.NewStartDate).WithMessage("New end date must be after new start date");

        RuleFor(x => x.NewPremium)
            .GreaterThanOrEqualTo(0).WithMessage("New premium must be zero or positive");

        RuleFor(x => x.NewCoverageAmount)
            .GreaterThanOrEqualTo(0).WithMessage("New coverage amount must be zero or positive")
            .When(x => x.NewCoverageAmount.HasValue);

        RuleFor(x => x.NewDeductible)
            .GreaterThanOrEqualTo(0).WithMessage("New deductible must be zero or positive")
            .When(x => x.NewDeductible.HasValue);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Notes));
    }
}
