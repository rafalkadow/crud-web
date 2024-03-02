using Application.Modules.Account.Create;
using Domain.Modules.Account.Commands;
using Shared.Helpers;
using Domain.Modules.Base.Enums;
using Shared.Extensions.EnumExtensions;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.Account.Create
{
    [TestFixture, SingleThreaded]
    public class AccountHandlerTestCreate : BaseHandlerBase
    {

        public AccountHandlerTestCreate()
            : base()
        {
        }

        [Test]
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
                Assert.That(result.OperationStatus);
            }

            Assert.That(AccountCount + countRecord == _dbContext.Account.Count(), Is.True);
        }

        [Test, Theory]
        [TestCase("00000000-0000-0000-0000-000000000001")]
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

            var foo = Assert.ThrowsAsync<InvalidOperationException>(
               async () =>
               {
                   var exception = await handler.Handle(item, CancellationToken.None);
               });
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000002")]
        [TestCase("00000000-0000-0000-0000-000000000003")]
        [TestCase("00000000-0000-0000-0000-000000000004")]
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
            Assert.That(result.OperationStatus);

            var foo = Assert.ThrowsAsync<ArgumentException>(
               async () =>
               {
                   var exception = await handler.Handle(item, CancellationToken.None);
               });
        }
    }
}