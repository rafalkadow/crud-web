using Domain.Modules.User;
using FluentValidation;
using Infrastructure.Shared.Models;

namespace Application.Validations
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(ur => ur.Password)
                .NotNull()
                    .WithMessage("Password should not be null")
                .Length(AppConstants.Validations.User.PasswordMinLength, AppConstants.Validations.User.PasswordMaxLength)
                    .WithMessage("Password length must be between {MinLength} and {MaxLength} chars");

            RuleFor(ur => ur.UserName)
                .NotNull()
                    .WithMessage("UserName should not be null")
                .Length(AppConstants.Validations.User.UsernameMinLength, AppConstants.Validations.User.UsernameMaxLength)
                    .WithMessage("UserName length must be between {MinLength} and {MaxLength} chars");
        }
    }
}