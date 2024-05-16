using Domain.Modules.ProductStock;
using FluentValidation;
using Infrastructure.Shared.Models;

namespace Application.Validations
{
    public class StockValidator : AbstractValidator<ProductStockWriteDto>
    {
        public StockValidator()
        {
            RuleFor(s => s.Quantity)
                .NotNull()
                    .WithMessage("Quantity cannot be null")
                .GreaterThanOrEqualTo(AppConstants.Validations.Stock.QuantityMinValue)
                    .WithMessage("Quantity must be greater than or equal to {ComparisonValue}");
        }
    }
}