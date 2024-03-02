using Application.Modules.Base.Validations;
using Domain.Modules.Base.Models;
using Domain.Modules.Product.Commands;
using Domain.Modules.Product.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Domain.Interfaces;

namespace Application.Modules.Product.Update
{
    [Serializable]
    public class UpdateProductValidation : BaseValidation<UpdateProductCommand>
    {
        public UpdateProductValidation(IDbContext dbContext, IDefinitionModel definitionModel)
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

            RuleFor(u => u.CategoryOfProductId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Enter the field value 'CategoryOfProductId'");

            RuleFor(f => f.DateUtc).Cascade(CascadeMode.Stop).NotEmpty()
                    .Must(date => date != default(DateTime))
                    .WithMessage("The 'DateUtc' is required");

            RuleFor(f => f.DateTimeUtc).Cascade(CascadeMode.Stop).NotEmpty()
                    .Must(date => date != default(DateTime))
                    .WithMessage("The 'DateTimeUtc' is required");
        }

        private bool UniqueName(BaseProductCommand model, string name)
        {
            var result = false;
            result = !DbContext.GetQueryable<ProductModel>().AsNoTracking().Any(u => u.Id != model.Id && u.Name == name);
            return result;
        }

        private bool UniqueCode(BaseProductCommand model, string code)
        {
            var result = false;
            result = !DbContext.GetQueryable<ProductModel>().AsNoTracking().Any(u => u.Id != model.Id && u.Code == code);
            return result;
        }
    }
}