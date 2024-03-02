using Domain.Modules.Account.Commands;
using Shared.Helpers;
using Application.Modules.Account.Update;
using Domain.Modules.Base.Enums;
using Shared.Extensions.EnumExtensions;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Account.Update
{
    [TestClass]
    public class AccountHandlerTestUpdate : BaseHandlerBase
    {

        public AccountHandlerTestUpdate()
        : base()
        {
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public void Handler_ReturnsSuccess_Update(string guid)
        {
            var handler = new UpdateAccountHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new UpdateAccountCommand
            {
                Id = new Guid(guid),
                AccountEmail = randomString,
                AccountPassword = randomString,
                AccountTypeId = (int)AccountTypeEnum.Administrator,
                AccountTypeName = AccountTypeEnum.Administrator.GetDescription(),
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.OperationStatus);

        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000002")]
        [DataRow("00000000-0000-0000-0000-000000000003")]
        [DataRow("00000000-0000-0000-0000-000000000004")]
        [DataRow("00000000-0000-0000-0000-000000000005")]
        public void Handler_Error_Update(string guid)
        {
            var handler = new UpdateAccountHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new UpdateAccountCommand
            {
                Id = new Guid(guid),
                AccountEmail = randomString,
                AccountPassword = randomString,
                AccountTypeId = (int)AccountTypeEnum.Administrator,
                AccountTypeName = AccountTypeEnum.Administrator.GetDescription(),
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsFalse(result.OperationStatus);

        }
    }
}