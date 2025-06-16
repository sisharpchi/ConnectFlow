using Application.Dtos;
using FluentValidation;

namespace Application.FluentValidators;

public class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
{
    public UpdateContactDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(2)
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MinimumLength(2)
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Address)
            .MinimumLength(5)
            .When(x => !string.IsNullOrWhiteSpace(x.Address));
    }
}
