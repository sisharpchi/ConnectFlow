using Application.Dtos;
using FluentValidation;

namespace Application.FluentValidators;

public class UpdateRoleDtoValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleDtoValidator()
    {
        RuleFor(x => x.Name)
           .MinimumLength(3)
           .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.Description)
            .MinimumLength(5)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
