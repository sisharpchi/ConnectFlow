using Application.Dtos;
using FluentValidation;

namespace Application.FluintValidation;

public class UserLogInDtoValidator : AbstractValidator<UserLogInDto>
{
    // Corrected constructor name to match the class name
    public UserLogInDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
