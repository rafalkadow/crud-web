﻿using Domain.Modules.QueryStringParameters;
using FluentValidation;
using Infrastructure.Shared.Models;

namespace Application.Validations
{
    public class RoleParametersValidator : AbstractValidator<RoleParametersDto>
    {
        public RoleParametersValidator()
        {
            Include(new QueryStringParameterValidator());
                        
            RuleFor(r => r.Name)
                .MaximumLength(AppConstants.Validations.Role.NameMaxLength)
                    .WithMessage("Name maximum lenght is {MaxLength} chars");

            //RuleForEach(r => r.UserId)
            //    .GreaterThanOrEqualTo(1)
            //        .WithMessage("RoleId must be greater than or equal to {ComparisonValue}");
        }
    }
}