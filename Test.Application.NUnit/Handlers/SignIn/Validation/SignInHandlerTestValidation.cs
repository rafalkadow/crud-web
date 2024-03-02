using Domain.Modules.SignIn.Commands;
using Domain.Modules.Base.Models;
using Shared.Enums;
using FluentValidation.TestHelper;
using Application.Modules.SignIn.Validations;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.SignIn.Validation
{
    [TestFixture]
    public class SignInHandlerTestValidation : BaseHandlerBase
    {
        public SignInHandlerTestValidation()
        : base()
        {
        }

        [Test]
        [TestCase("name1", "")]
        [TestCase("", "code2")]
        [TestCase("", "")]
        public void Validate_Error(string email, string password)
        {
            // arrange
            var definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);

            var model = new SignInCommand
            {
                SignInEmail = email,
                SignInPassword = password,
            };

            var validator = new SignInValidation(_dbContext, definitionModel);

            // act
            var result = validator.TestValidate(model);
            // assert
            result.ShouldHaveAnyValidationError();
        }


        [Test]
        [TestCase("test1@test.com", "pass123")]
        public void Validate_Success(string email, string password)
        {
            // arrange
            var definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);

            var model = new SignInCommand
            {
                SignInEmail = email,
                SignInPassword = password,
            };

            var validator = new SignInValidation(_dbContext, definitionModel);

            // act
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}