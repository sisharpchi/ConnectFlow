using Application.Dtos;
using FluentValidation;

namespace Application.FluentValidators;

public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Rol nomi kiritilishi shart")
           .MinimumLength(3).WithMessage("Rol nomi kamida 3 ta belgidan iborat bo‘lishi kerak")
           .MaximumLength(50).WithMessage("Rol nomi 50 belgidan oshmasligi kerak");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Tavsif kiritilishi shart")
            .MaximumLength(200).WithMessage("Tavsif 200 belgidan oshmasligi kerak");
    }
}
