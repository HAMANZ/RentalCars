using FleetErp.Application.Insurance.Dtos;
using FluentValidation;

namespace FleetErp.Application.Insurance.Validators;

public class CreateInsuranceCompanyRequestValidator : AbstractValidator<CreateInsuranceCompanyRequest>
{
    public CreateInsuranceCompanyRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Company name is required")
            .MaximumLength(150).WithMessage("Company name cannot exceed 150 characters");

        RuleFor(x => x.Phone)
            .MaximumLength(50).WithMessage("Phone cannot exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.Email)
            .MaximumLength(150).WithMessage("Email cannot exceed 150 characters")
            .EmailAddress().WithMessage("Invalid email format")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Address)
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Address));

        RuleFor(x => x.ContactPerson)
            .MaximumLength(150).WithMessage("Contact person name cannot exceed 150 characters")
            .When(x => !string.IsNullOrEmpty(x.ContactPerson));
    }
}

public class UpdateInsuranceCompanyRequestValidator : AbstractValidator<UpdateInsuranceCompanyRequest>
{
    public UpdateInsuranceCompanyRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Company name is required")
            .MaximumLength(150).WithMessage("Company name cannot exceed 150 characters");

        RuleFor(x => x.Phone)
            .MaximumLength(50).WithMessage("Phone cannot exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.Email)
            .MaximumLength(150).WithMessage("Email cannot exceed 150 characters")
            .EmailAddress().WithMessage("Invalid email format")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Address)
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Address));

        RuleFor(x => x.ContactPerson)
            .MaximumLength(150).WithMessage("Contact person name cannot exceed 150 characters")
            .When(x => !string.IsNullOrEmpty(x.ContactPerson));
    }
}
