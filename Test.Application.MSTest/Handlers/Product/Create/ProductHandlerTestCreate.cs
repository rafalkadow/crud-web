using Application.Modules.Product.Create;
using Domain.Modules.Product.Commands;
using Shared.Helpers;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Product.Create
{
    [TestClass]
    public class ProductHandlerTestCreate : BaseHandlerBase
    {

        public ProductHandlerTestCreate()
        : base()
        {
        }

        [TestMethod]
        public async Task Handler_ReturnsSuccess_CreateAsync()
        {
            var handler = new CreateProductHandler(_dbContext, _mapper, userAccessor);
            int ProductCount = _dbContext.Product.Count();
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            
            var countRecord = 100;

            for (int i = 0; i < countRecord; i++)
            {
                var randomString = generator.RandomString(3);
                var item = new CreateProductCommand
                {
                    Name = randomString,
                    Code = randomString,
                    CategoryOfProductId = _dbContext.CategoryOfProduct.First().Id,
                };

                var result = await handler.Handle(item, CancellationToken.None);
                Assert.IsTrue(result.OperationStatus);
            }

            Assert.AreEqual(ProductCount + countRecord, _dbContext.Product.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public async Task Handler_ReturnsError_CreateAsync(string guid)
        {
            var handler = new CreateProductHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new CreateProductCommand
            {
                Id = new Guid(guid),
                Name = randomString,
                Code = randomString,
                CategoryOfProductId = _dbContext.CategoryOfProduct.First().Id,
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
            var handler = new CreateProductHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new CreateProductCommand
            {
                Id = new Guid(guid),
                Name = randomString,
                Code = randomString,
            };
            var result = await handler.Handle(item, CancellationToken.None);
            Assert.IsTrue(result.OperationStatus);

            result = await handler.Handle(item, CancellationToken.None);
            Assert.IsTrue(result.OperationStatus);
        }
    }
}