using Shared.Helpers;
using Application.Modules.CategoryOfProduct.Queries;
using Domain.Modules.CategoryOfProduct.Queries;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.CategoryOfProduct.Queries
{
    [TestClass]
    public class CategoryOfProductHandlerTestQueries : BaseHandlerBase
    {

        public CategoryOfProductHandlerTestQueries()
        : base()
        {
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public void Handler_ReturnsSuccess_GetId(string guid)
        {
            var handler = new GetCategoryOfProductQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetCategoryOfProductQueryById(new Guid(guid));

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
            var handler = new GetCategoryOfProductQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetCategoryOfProductQueryById(new Guid(guid));

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void Handler_ReturnsSuccess_GetAll()
        {
            var handler = new GetCategoryOfProductQueryAllHandler(_dbContext, _mapper, userAccessor);

            var item = new GetCategoryOfProductQueryAll();

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void Handler_ReturnsError_GetAll()
        {
            var handler = new GetCategoryOfProductQueryAllHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(5);
            var item = new GetCategoryOfProductQueryAll();
            item.Name = randomString;
            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.Count() == 0);
        }
    }
}