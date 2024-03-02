using Shared.Helpers;
using Application.Modules.Account.Queries;
using Domain.Modules.Account.Queries;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.Account.Queries
{
    [TestFixture]
    public class AccountHandlerTestQueries : BaseHandlerBase
    {

        public AccountHandlerTestQueries()
        : base()
        {
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public void Handler_ReturnsSuccess_GetId(string guid)
        {
            var handler = new GetAccountQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetAccountQueryById(new Guid(guid));

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result.Id == new Guid(guid));
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000002")]
        [TestCase("00000000-0000-0000-0000-000000000003")]
        [TestCase("00000000-0000-0000-0000-000000000004")]
        [TestCase("00000000-0000-0000-0000-000000000005")]
        public void Handler_ReturnsError_GetId(string guid)
        {
            var handler = new GetAccountQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetAccountQueryById(new Guid(guid));

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result == null);
        }

        [Test]
        public void Handler_ReturnsSuccess_GetAll()
        {
            var handler = new GetAccountQueryAllHandler(_dbContext, _mapper, userAccessor);

            var item = new GetAccountQueryAll();

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result.Count() > 0);
        }

        [Test]
        public void Handler_ReturnsError_GetAll()
        {
            var handler = new GetAccountQueryAllHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);
            var item = new GetAccountQueryAll();
            item.AccountEmail = randomString;
            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result.Count() == 0);
        }
    }
}