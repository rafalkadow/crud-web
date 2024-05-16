using Application.Modules.CategoryOfProduct.Create;
using Domain.Modules.CategoryOfProduct.Commands;
using Shared.Helpers;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.CategoryOfProduct.Create
{
    [TestFixture]
    public class CategoryOfProductHandlerTestCreate : BaseHandlerBase
    {

        public CategoryOfProductHandlerTestCreate()
        : base()
        {
        }

        [Test]
        public async Task Handler_ReturnsSuccess_CreateAsync()
        {
            var handler = new CreateCategoryOfProductHandler(_dbContext, _mapper, userAccessor);
            int CategoryOfProductCount = _dbContext.CategoryOfProduct.Count();
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            
            var countRecord = 100;

            for (int i = 0; i < countRecord; i++)
            {
                var randomString = generator.RandomString(3);
                var item = new CreateCategoryOfProductCommand
                {
                    Name = randomString,
                    Code = randomString,
                };

                var result = await handler.Handle(item, CancellationToken.None);
                Assert.That(result.Success);
            }

            Assert.That(CategoryOfProductCount + countRecord == _dbContext.CategoryOfProduct.Count());
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public async Task Handler_ReturnsError_CreateAsync(string guid)
        {
            var handler = new CreateCategoryOfProductHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new CreateCategoryOfProductCommand
            {
                Id = new Guid(guid),
                Name = randomString,
                Code = randomString,
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
            var handler = new CreateCategoryOfProductHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new CreateCategoryOfProductCommand
            {
                Id = new Guid(guid),
                Name = randomString,
                Code = randomString,
            };
            var result = await handler.Handle(item, CancellationToken.None);
            Assert.That(result.Success);

            var foo = Assert.ThrowsAsync<ArgumentException>(
               async () =>
               {
                   var exception = await handler.Handle(item, CancellationToken.None);
               });
        }
    }
}