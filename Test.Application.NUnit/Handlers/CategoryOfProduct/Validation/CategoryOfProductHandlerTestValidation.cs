using Application.Modules.CategoryOfProduct.Create;
using Domain.Modules.CategoryOfProduct.Commands;
using Domain.Modules.Base.Models;
using Shared.Enums;
using FluentValidation.TestHelper;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.CategoryOfProduct.Validation
{
    [TestFixture]
    public class CategoryOfProductHandlerTestValidation : BaseHandlerBase
    {

        public CategoryOfProductHandlerTestValidation()
        : base()
        {
        }

        [Test]
        [TestCase("name1", "")]
        [TestCase("", "code2")]
        [TestCase("", "")]
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


        [Test]
        [TestCase("name1", "code1")]
        [TestCase("name2", "code2")]
        [TestCase("name3", "code3")]
        [TestCase("name4", "code4")]
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