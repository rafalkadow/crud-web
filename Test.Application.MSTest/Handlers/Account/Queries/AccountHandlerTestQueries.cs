using Shared.Helpers;
using Application.Modules.Account.Queries;
using Domain.Modules.Account.Queries;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Account.Queries
{
    [TestClass]
    public class AccountHandlerTestQueries : BaseHandlerBase
    {

        public AccountHandlerTestQueries()
        : base()
        {
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public void Handler_ReturnsSuccess_GetId(string guid)
        {
            var handler = new GetAccountQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetAccountQueryById(new Guid(guid));

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.Id == new Guid(guid));
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000002")]
        [DataRow("00000000-0000-0000-0000-000000000003")]
        [DataRow("00000000-0000-0000-0000-000000000004")]
        [DataRow("00000000-0000-0000-0000-000000000005")]
        public void Handler_ReturnsError_GetId(string guid)
        {
            var handler = new GetAccountQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetAccountQueryById(new Guid(guid));

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void Handler_ReturnsSuccess_GetAll()
        {
            var handler = new GetAccountQueryAllHandler(_dbContext, _mapper, userAccessor);

            var item = new GetAccountQueryAll();

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void Handler_ReturnsError_GetAll()
        {
            var handler = new GetAccountQueryAllHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);
            var item = new GetAccountQueryAll();
            item.AccountEmail = randomString;
            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.Count() == 0);
        }
    }
}