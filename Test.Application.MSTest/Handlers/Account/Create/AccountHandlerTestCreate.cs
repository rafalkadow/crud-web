using Application.Modules.Account.Create;
using Domain.Modules.Account.Commands;
using Shared.Helpers;
using Domain.Modules.Base.Enums;
using Shared.Extensions.EnumExtensions;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Account.Create
{
    [TestClass]
    public class SignInHandlerTestCreate : BaseHandlerBase
    {
  
        public SignInHandlerTestCreate()
            : base()
        {
        }

        [TestMethod]
        public async Task Handler_ReturnsSuccess_CreateAsync()
        {
            var handler = new CreateAccountHandler(_dbContext, _mapper, userAccessor);
            int AccountCount = _dbContext.Account.Count();
            var generator = new RandomGenerator();
            
            var countRecord = 100;

            for (int i = 0; i < countRecord; i++)
            {
                var randomString = generator.RandomString(5);
                var item = new CreateAccountCommand
                {
                    AccountEmail = randomString,
                    AccountPassword = randomString,
                    FirstName = randomString,
                    LastName = randomString,
                    AccountTypeId = (int)AccountTypeEnum.Administrator,
                    AccountTypeName = AccountTypeEnum.Administrator.GetDescription(),
                };
                var result = await handler.Handle(item, CancellationToken.None);
                Assert.IsTrue(result.OperationStatus);
            }

            Assert.AreEqual(AccountCount + countRecord, _dbContext.Account.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public async Task Handler_ReturnsError_CreateAsync(string guid)
        {
            var handler = new CreateAccountHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomString = generator.RandomString(5);

            var item = new CreateAccountCommand
            {
                Id = new Guid(guid),
                AccountEmail = randomString,
                AccountPassword = randomString,
                FirstName = randomString,
                LastName = randomString,
                AccountTypeId = (int)AccountTypeEnum.Administrator,
                AccountTypeName = AccountTypeEnum.Administrator.GetDescription(),
            };
            var result = await handler.Handle(item, CancellationToken.None);
            Assert.IsTrue(result.OperationStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("00000000-0000-0000-0000-000000000002")]
        [DataRow("00000000-0000-0000-0000-000000000003")]
        [DataRow("00000000-0000-0000-0000-000000000004")]
        public async Task Handler_ReturnsError2_CreateAsync(string guid)
        {
            var handler = new CreateAccountHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomString = generator.RandomString(5);

            var item = new CreateAccountCommand
            {
                Id = new Guid(guid),
                AccountEmail = randomString,
                AccountPassword = randomString,
                FirstName = randomString,
                LastName = randomString,
                AccountTypeId = (int)AccountTypeEnum.Administrator,
                AccountTypeName = AccountTypeEnum.Administrator.GetDescription(),
            };
            var result = await handler.Handle(item, CancellationToken.None);
            Assert.IsTrue(result.OperationStatus);

            result = await handler.Handle(item, CancellationToken.None);
            Assert.IsTrue(result.OperationStatus);
        }
    }
}