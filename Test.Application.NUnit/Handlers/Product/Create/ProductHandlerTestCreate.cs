using Application.Modules.Product.Create;
using Domain.Modules.Product.Commands;
using Shared.Helpers;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.Product.Create
{
    [TestFixture]
    public class ProductHandlerTestCreate : BaseHandlerBase
    {

        public ProductHandlerTestCreate()
            : base()
        {
        }

        [Test]
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
                Assert.That(result.OperationStatus);
            }

            Assert.That(ProductCount + countRecord == _dbContext.Product.Count());
        }

        [Theory]
        [TestCase("00000000-0000-0000-0000-000000000001")]
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

            var foo = Assert.ThrowsAsync<InvalidOperationException>(
               async () =>
               {
                   var exception = await handler.Handle(item, CancellationToken.None);
               });
        }

        [Theory]
        [TestCase("00000000-0000-0000-0000-000000000002")]
        [TestCase("00000000-0000-0000-0000-000000000003")]
        [TestCase("00000000-0000-0000-0000-000000000004")]
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
            Assert.That(result.OperationStatus);

            var foo = Assert.ThrowsAsync<ArgumentException>(
                async () =>
                {
                    var exception = await handler.Handle(item, CancellationToken.None);
                    Console.WriteLine(exception.OperationStatus);
                });
        }
    }
}