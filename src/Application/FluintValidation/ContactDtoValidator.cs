using Application.Dtos;
using FluentValidation;

namespace Application.FluintValidation;

public class ContactDtoValidator : AbstractValidator<ContactDto>
{
    public ContactDtoValidator()
    {
        RuleFor(x => x.ContactId)
            .GreaterThan(0).WithMessage("ContactId must be greater than 0.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.")
            .Matches("^[A-Za-z'-]+$").WithMessage("First name can only contain letters, hyphens, or apostrophes.");

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
            .Matches("^[A-Za-z'-]*$").WithMessage("Last name can only contain letters, hyphens, or apostrophes.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Email format is not correct.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+998\d{9}$").WithMessage("Phone number must be in the format +998901234567.");

        RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.")
            .Matches(@"^[\w\s\.,#-]*$").WithMessage("Address contains invalid characters.");
    }
}
