using Application.Modules.Base.Validations;
using Domain.Modules.Base.Models;
using FluentValidation;
using Domain.Interfaces;
using Domain.Modules.Account.Commands;
using Domain.Modules.Account;

namespace Application.Modules.Account.Validations
{
    [Serializable]
    public class UpdateAccountValidation : BaseValidation<UpdateAccountCommand>
    {
        public UpdateAccountValidation(IDbContext dbContext, IDefinitionModel definitionModel)
            : base(dbContext, definitionModel)
        {
            RuleFor(u => u.AccountEmail).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Enter the field value 'Email'")
                   .Must(x => x.Length >= 5 && x.Length <= 200).WithMessage("Email should be between 5 and 200 characters")
                   .Must(UniqueEmail)
                   .WithMessage("The 'Email' field must be unique");
        }

        private bool UniqueEmail(BaseAccountCommand model, string email)
        {
            var result = false;
            result = !DbContext.GetQueryable<AccountModel>().Any(u => u.Id != model.Id && u.AccountEmail == email);
            return result;
        }
    }
}