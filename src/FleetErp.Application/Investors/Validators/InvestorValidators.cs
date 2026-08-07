using FleetErp.Application.Investors.Dtos;
using FluentValidation;

namespace FleetErp.Application.Investors.Validators;

public class CreateInvestorRequestValidator : AbstractValidator<CreateInvestorRequest>
{
    public CreateInvestorRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(150).WithMessage("Full name cannot exceed 150 characters");

        RuleFor(x => x.Phone)
            .MaximumLength(50).WithMessage("Phone cannot exceed 50 characters");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format")
            .MaximumLength(150).WithMessage("Email cannot exceed 150 characters");

        RuleFor(x => x.NationalId)
            .MaximumLength(50).WithMessage("National ID cannot exceed 50 characters");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");
    }
}

public class UpdateInvestorRequestValidator : AbstractValidator<UpdateInvestorRequest>
{
    public UpdateInvestorRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(150).WithMessage("Full name cannot exceed 150 characters");

        RuleFor(x => x.Phone)
            .MaximumLength(50).WithMessage("Phone cannot exceed 50 characters");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format")
            .MaximumLength(150).WithMessage("Email cannot exceed 150 characters");

        RuleFor(x => x.NationalId)
            .MaximumLength(50).WithMessage("National ID cannot exceed 50 characters");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");
    }
}

public class CreateInvestorDocumentRequestValidator : AbstractValidator<CreateInvestorDocumentRequest>
{
    public CreateInvestorDocumentRequestValidator()
    {
        RuleFor(x => x.DocumentTypeId)
            .GreaterThan(0).WithMessage("Document type is required");

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage("File path is required")
            .MaximumLength(500).WithMessage("File path cannot exceed 500 characters");
    }
}
