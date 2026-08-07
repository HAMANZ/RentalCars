using FleetErp.Application.Customers.Dtos;
using FluentValidation;

namespace FleetErp.Application.Customers.Validators;

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(150).WithMessage("Full name cannot exceed 150 characters");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters");

        RuleFor(x => x.Email)
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters")
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Invalid email format");

        RuleFor(x => x.NationalId)
            .MaximumLength(50).WithMessage("National ID cannot exceed 50 characters");

        RuleFor(x => x.DrivingLicenseNumber)
            .MaximumLength(50).WithMessage("Driving license number cannot exceed 50 characters");

        RuleFor(x => x.Address)
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");
    }
}

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(150).WithMessage("Full name cannot exceed 150 characters");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters");

        RuleFor(x => x.Email)
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters")
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Invalid email format");

        RuleFor(x => x.NationalId)
            .MaximumLength(50).WithMessage("National ID cannot exceed 50 characters");

        RuleFor(x => x.DrivingLicenseNumber)
            .MaximumLength(50).WithMessage("Driving license number cannot exceed 50 characters");

        RuleFor(x => x.Address)
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");
    }
}

public class CreateCustomerDocumentRequestValidator : AbstractValidator<CreateCustomerDocumentRequest>
{
    public CreateCustomerDocumentRequestValidator()
    {
        RuleFor(x => x.DocumentTypeId)
            .GreaterThan(0).WithMessage("Document type is required");

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage("File path is required")
            .MaximumLength(500).WithMessage("File path cannot exceed 500 characters");
    }
}
