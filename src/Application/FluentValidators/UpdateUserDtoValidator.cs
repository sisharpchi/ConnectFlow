using Application.Dtos;
using FluentValidation;

namespace Application.FluentValidators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.FirstName)
         .MaximumLength(50).WithMessage("Ism 50 belgidan oshmasligi kerak")
         .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Familiya 50 belgidan oshmasligi kerak")
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+998\d{9}$").WithMessage("Telefon raqam formati: +998901234567")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.UserName)
            .MinimumLength(3).WithMessage("Foydalanuvchi nomi kamida 3 ta belgidan iborat bo‘lishi kerak")
            .MaximumLength(20).WithMessage("Foydalanuvchi nomi 20 belgidan oshmasligi kerak")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Faqat harf, raqam va pastki chiziq (_) bo‘lishi mumkin")
            .When(x => !string.IsNullOrWhiteSpace(x.UserName));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email noto‘g‘ri formatda")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}
