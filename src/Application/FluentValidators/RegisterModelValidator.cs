using Application.Dtos.Auth;
using FluentValidation;

namespace Application.FluentValidators;

public class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        RuleFor(model => model.FirstName)
            .NotNull().WithMessage("First name can not be null")
            .NotEmpty().WithMessage("First name can not be empty")
            .MinimumLength(3).WithMessage("First name's length must be minimum 3")
            .MaximumLength(50).WithMessage("First name's length must be maximum 50")
            .Must(BeAValid).WithMessage("First name can not contain non-letter characters")
            .Must(StartWithUpperLetter).WithMessage("First name must start with upper letter");


        RuleFor(model => model.LastName)
            .NotNull().WithMessage("Last name can not be null")
            .NotEmpty().WithMessage("Last name can not be empty")
            .MinimumLength(3).WithMessage("Last name's length must be minimum 3")
            .MaximumLength(50).WithMessage("Last name's length must be maximum 50")
            .Must(BeAValid).WithMessage("Lasr name can not contain non-letter characters")
            .Must(StartWithUpperLetter).WithMessage("Last name must start with upper letter");


        RuleFor(model => model.PhoneNumber)
            .NotEmpty().WithMessage("Telefon raqam kiritilishi shart")
            .Matches(@"^\+?\d{9,15}$").WithMessage("Telefon raqam noto‘g‘ri formatda");

        RuleFor(model => model.UserName)
            .NotEmpty().WithMessage("Foydalanuvchi nomi kiritilishi shart")
            .MinimumLength(3).WithMessage("Kamida 3 ta belgidan iborat bo‘lishi kerak")
            .MaximumLength(20).WithMessage("Ko‘pi bilan 20 ta belgidan oshmasligi kerak")
            .Matches("^[a-zA-Z0-9_]*$").WithMessage("Faqat harf, raqam va pastki chiziq (_) ruxsat etiladi")
            .Must(name => !name.ToLower().Contains("admin"))
            .WithMessage("Foydalanuvchi nomida 'admin' so‘zi bo‘lmasligi kerak");


        RuleFor(model => model.UserName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Foydalanuvchi nomi kiritilishi shart")
            .MinimumLength(3).WithMessage("Kamida 3 ta belgidan iborat bo‘lishi kerak")
            .MaximumLength(20).WithMessage("Ko‘pi bilan 20 ta belgidan oshmasligi kerak")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Faqat harf, raqam va pastki chiziq (_) bo‘lishi mumkin")
            .Must(name => !name.ToLower().Contains("admin"))
            .WithMessage("Foydalanuvchi nomida 'admin' so‘zi bo‘lmasligi kerak");



        RuleFor(model => model.Password)
            .NotEmpty().WithMessage("Parol kiritilishi shart")
            .MinimumLength(6).WithMessage("Parol kamida 6 ta belgidan iborat bo‘lishi kerak")
            .MaximumLength(100).WithMessage("Parol 100 ta belgidan oshmasligi kerak")
            .Matches(@"[A-Z]").WithMessage("Parolda kamida bitta katta harf bo‘lishi kerak")
            .Matches(@"[a-z]").WithMessage("Parolda kamida bitta kichik harf bo‘lishi kerak")
            .Matches(@"\d").WithMessage("Parolda kamida bitta raqam bo‘lishi kerak")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&\+\=]").WithMessage("Parolda kamida bitta maxsus belgi (! ? * . @ # $ % ^ & + =) bo‘lishi kerak");


    }


    private bool BeAValid(string firstname)
    {

        for (int i = 0; i < firstname.Length; i++)
        {

            if (firstname[i] >= 65 && firstname[i] <= 90)
                continue;

            if (firstname[i] >= 97 && firstname[i] <= 122)
                continue;
            else
                return false;
        }
        return true;
    }



    private bool StartWithUpperLetter(string firstname)
    {
        for (int i = 0; i < firstname.Length; i++)
        {
            if (!char.IsUpper(firstname[i])) return false;
        }
        return true;
    }
}
