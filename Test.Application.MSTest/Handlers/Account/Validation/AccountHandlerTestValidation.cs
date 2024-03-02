using Domain.Modules.Account.Commands;
using Domain.Modules.Base.Models;
using Shared.Enums;
using FluentValidation.TestHelper;
using Domain.Modules.Base.Enums;
using Application.Modules.Account.Validations;
using Shared.Extensions.EnumExtensions;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Account.Validation
{
    [TestClass]
    public class AccountHandlerTestValidation : BaseHandlerBase
    {

        public AccountHandlerTestValidation()
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


        [TestMethod]
        [DataRow("name1", "code1")]
        [DataRow("name2", "code2")]
        [DataRow("name3", "code3")]
        [DataRow("name4", "code4")]
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


        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001", "name1", "code1")]
        public void Validate_Error_Guid(string guid, string name, string code)
        {
            // arrange
            var definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);

            var model = new CreateAccountCommand
            {
                Id = new Guid(guid),
                AccountEmail = name,
                AccountPassword = code,
                AccountTypeId = (int)AccountTypeEnum.Administrator,
                AccountTypeName = AccountTypeEnum.Administrator.GetDescription(),
            };

            var validator = new CreateAccountValidation(_dbContext, definitionModel);

            // act
            var result = validator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001", "name1", "code1")]
        public void Validate_Success_Guid(string guid, string name, string code)
        {
            // arrange
            var definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);

            var model = new UpdateAccountCommand
            {
                Id = new Guid(guid),
                AccountEmail = name,
                AccountPassword = code,
                AccountTypeId = (int)AccountTypeEnum.Administrator,
                AccountTypeName = AccountTypeEnum.Administrator.GetDescription(),
            };

            var validator = new UpdateAccountValidation(_dbContext, definitionModel);

            // act
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}