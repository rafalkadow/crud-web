using Application.Modules.CategoryOfProduct.Create;
using Domain.Modules.CategoryOfProduct.Commands;
using Domain.Modules.Base.Models;
using Shared.Enums;
using FluentValidation.TestHelper;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.CategoryOfProduct.Validation
{
    [TestClass]
    public class CategoryOfProductHandlerTestValidation : BaseHandlerBase
    {

        public CategoryOfProductHandlerTestValidation()
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

            var model = new CreateCategoryOfProductCommand
            {
                Name = name,
                Code = code,
            };

            var validator = new CreateCategoryOfProductValidation(_dbContext, definitionModel);

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

            var model = new CreateCategoryOfProductCommand
            {
                Name = name,
                Code = code,
            };

            var validator = new CreateCategoryOfProductValidation(_dbContext, definitionModel);

            // act
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}