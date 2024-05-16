using Domain.Modules.Role;
using FluentValidation;
using Infrastructure.Shared.Models;

namespace Application.Validations
{
    public class RoleValidator : AbstractValidator<RoleWriteDto>
    {
        public RoleValidator()
        {
            RuleFor(r => r.Name)
                .NotNull().WithMessage("Name cannot be null")
                .NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(AppConstants.Validations.Role.NameMaxLength).WithMessage("Name maximum lenght is {MaxLength}");
        }
    }
}