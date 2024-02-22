using Domain.Modules.Account.Commands;
using Domain.Modules.Base.Models;
using Shared.Enums;
using FluentValidation.TestHelper;
using Web.Integration.Handlers.OrderHeader;
using Domain.Modules.Base.Enums;
using Application.Modules.Account.Validations;
using Shared.Extensions.EnumExtensions;

namespace Test.Application.Xunit.Handlers.Account.Validation
{
    public class AccountHandlerTestValidation : BaseHandlerBase
    {

        public AccountHandlerTestValidation()
        : base()
        {
        }

        [Theory]
        [InlineData("name1", "")]
        [InlineData("", "code2")]
        [InlineData("", "")]
        public void Validate_Error(string name, string code)
        {
            // arrange
            var definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);

            var model = new CreateAccountCommand
            {
                AccountEmail = name,
                AccountPassword = code,
                AccountTypeId = (int)AccountTypeEnum.Administrator,
                AccountTypeName = AccountTypeEnum.Administrator.GetDescription(),
            };

            var validator = new CreateAccountValidation(_dbContext, definitionModel);

            // act
            var result = validator.TestValidate(model);
            // assert
            result.ShouldHaveAnyValidationError();
        }


        [Theory]
        [InlineData("name1", "code1")]
        [InlineData("name2", "code2")]
        [InlineData("name3", "code3")]
        [InlineData("name4", "code4")]
        public void Validate_Success(string name, string code)
        {
            // arrange
            var definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);

            var model = new CreateAccountCommand
            {
                AccountEmail = name,
                AccountPassword = code,
                AccountTypeId = (int)AccountTypeEnum.Administrator,
                AccountTypeName = AccountTypeEnum.Administrator.GetDescription(),
            };

            var validator = new CreateAccountValidation(_dbContext, definitionModel);

            // act
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}