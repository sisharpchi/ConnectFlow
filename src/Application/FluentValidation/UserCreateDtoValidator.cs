using Application.Dtos;
using FluentValidation;

namespace Application.FluintValidation;

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.")
            .Matches("^[A-Za-z'-]+$").WithMessage("First name can only contain letters, hyphens, or apostrophes.");

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
            .Matches("^[A-Za-z'-]*$").WithMessage("Last name can only contain letters, hyphens, or apostrophes.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(4).WithMessage("Username must be at least 4 characters.")
            .MaximumLength(30).WithMessage("Username must not exceed 30 characters.")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Email must be a valid format (e.g., example@mail.com).");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+998\d{9}$").WithMessage("Phone number must be in the format +998901234567.");
    }
}
