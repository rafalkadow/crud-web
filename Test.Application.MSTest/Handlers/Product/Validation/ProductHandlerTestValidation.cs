using Application.Modules.Product.Create;
using Domain.Modules.Product.Commands;
using Domain.Modules.Base.Models;
using Shared.Enums;
using FluentValidation.TestHelper;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Product.Validation
{
    [TestClass]
    public class ProductHandlerTestValidation : BaseHandlerBase
    {

        public ProductHandlerTestValidation()
        : base()
        {
        }

        [TestMethod]
        [DataRow("name1", "")]
        [DataRow("", "code2")]
        [DataRow("", "")]
        public void Validate_Error(string name, string code)
        {
            // arrange
            var definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);

            var model = new CreateProductCommand
            {
                Name = name,
                Code = code,
                CategoryOfProductId = _dbContext.CategoryOfProduct.First().Id,
            };

            var validator = new CreateProductValidation(_dbContext, definitionModel);

            // act
            var result = validator.TestValidate(model);
            // assert
            result.ShouldHaveAnyValidationError();
        }


        [TestMethod]
        [DataRow("name1", "code1")]
        [DataRow("name2", "code2")]
        [DataRow("name3", "code3")]
        [DataRow("name4", "code4")]
        public void Validate_Success(string name, string code)
        {
            // arrange
            var definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);

            var model = new CreateProductCommand
            {
                Name = name,
                Code = code,
                CategoryOfProductId = _dbContext.CategoryOfProduct.First().Id,
            };

            var validator = new CreateProductValidation(_dbContext, definitionModel);

            // act
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}