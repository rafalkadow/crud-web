using Application.Modules.Base.Validations;
using Domain.Modules.Base.Models;
using Domain.Modules.CategoryOfProduct.Commands;
using Domain.Modules.CategoryOfProduct.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Domain.Interfaces;

namespace Application.Modules.CategoryOfProduct.Create
{
    [Serializable]
    public class CreateCategoryOfProductValidation : BaseValidation<CreateCategoryOfProductCommand>
    {
        public CreateCategoryOfProductValidation(IDbContext dbContext, IDefinitionModel definitionModel)
            : base(dbContext, definitionModel)
        {
            RuleFor(u => u.Name).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Enter the field value 'Name'")
                   .Must(x => x.Length >= 1 && x.Length <= 20).WithMessage("Name should be between 1 and 20 characters")
                   .Must(UniqueName)
                   .WithMessage("The 'Name' field must be unique");

            RuleFor(u => u.Code).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Enter the field value 'Code'")
                   .Must(x => x.Length >= 1 && x.Length <= 20).WithMessage("Code should be between 1 and 20 characters")
                   .Must(UniqueCode)
                   .WithMessage("The 'Code' field must be unique");
        }

        private bool UniqueName(BaseCategoryOfProductCommand model, string name)
        {
            var result = false;
            result = !DbContext.GetQueryable<CategoryOfProductModel>().AsNoTracking().Any(u => u.Id != model.Id && u.Name == name);
            return result;
        }

        private bool UniqueCode(BaseCategoryOfProductCommand model, string code)
        {
            var result = false;
            result = !DbContext.GetQueryable<CategoryOfProductModel>().AsNoTracking().Any(u => u.Id != model.Id && u.Code == code);
            return result;
        }
    }
}