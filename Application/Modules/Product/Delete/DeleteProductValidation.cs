using Application.Modules.Base.Validations;
using Domain.Modules.Base.Models;
using Domain.Modules.Product.Commands;
using FluentValidation;
using Domain.Interfaces;

namespace Application.Modules.Product.Delete
{
    [Serializable]
    public class DeleteProductValidation : BaseValidation<DeleteProductCommand>
    {
        public DeleteProductValidation(IDbContext dbContext, IDefinitionModel definitionModel)
            : base(dbContext, definitionModel)
        {
            RuleFor(x => x.GuidList).NotNull().NotEmpty().WithMessage("The list 'Id' field doesn't be empty");
        }
    }
}