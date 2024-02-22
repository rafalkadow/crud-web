using Application.Modules.Base.Validations;
using Domain.Modules.Base.Models;
using Domain.Modules.CategoryOfProduct.Commands;
using FluentValidation;
using Domain.Interfaces;

namespace Application.Modules.CategoryOfProduct.Delete
{
    [Serializable]
    public class DeleteCategoryOfProductValidation : BaseValidation<DeleteCategoryOfProductCommand>
    {
        public DeleteCategoryOfProductValidation(IDbContext dbContext, IDefinitionModel definitionModel)
            : base(dbContext, definitionModel)
        {
            RuleFor(x => x.GuidList).NotNull().NotEmpty().WithMessage("The list 'Id' field doesn't be empty");
        }
    }
}