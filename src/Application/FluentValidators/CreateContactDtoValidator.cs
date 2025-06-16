using Application.Dtos;
using FluentValidation;

namespace Application.FluentValidators;

public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
{
    public CreateContactDtoValidator()
    {
        RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("Ism kiritilishi shart")
           .MaximumLength(50).WithMessage("Ism 50 belgidan oshmasligi kerak");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Familiya kiritilishi shart")
            .MaximumLength(50).WithMessage("Familiya 50 belgidan oshmasligi kerak");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon raqam kiritilishi shart")
            .Matches(@"^\+998\d{9}$").WithMessage("Telefon raqam formati: +998901234567");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email noto‘g‘ri formatda")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage("Manzil 200 belgidan oshmasligi kerak");
    }
}
