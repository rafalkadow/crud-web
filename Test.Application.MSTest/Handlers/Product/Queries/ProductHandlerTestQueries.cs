using Shared.Helpers;
using Application.Modules.Product.Queries;
using Domain.Modules.Product.Queries;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Product.Queries
{
    [TestClass]
    public class ProductHandlerTestQueries : BaseHandlerBase
    {

        public ProductHandlerTestQueries()
        : base()
        {
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public void Handler_ReturnsSuccess_GetId(string guid)
        {
            var handler = new GetProductQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetProductQueryById(new Guid(guid));

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
            var handler = new GetProductQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetProductQueryById(new Guid(guid));

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void Handler_ReturnsSuccess_GetAll()
        {
            var handler = new GetProductQueryAllHandler(_dbContext, _mapper, userAccessor);

            var item = new GetProductQueryAll();

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void Handler_ReturnsError_GetAll()
        {
            var handler = new GetProductQueryAllHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);
            var item = new GetProductQueryAll();
            item.Name = randomString;
            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.Count() == 0);
        }
    }
}