using Domain.Modules.CategoryOfProduct.Commands;
using Shared.Helpers;
using Application.Modules.CategoryOfProduct.Update;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.CategoryOfProduct.Update
{
    [TestFixture]
    public class CategoryOfProductHandlerTestUpdate : BaseHandlerBase
    {

        public CategoryOfProductHandlerTestUpdate()
        : base()
        {
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public void Handler_ReturnsSuccess_Update(string guid)
        {
            var handler = new UpdateCategoryOfProductHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new UpdateCategoryOfProductCommand
            {
                Id = new Guid(guid),
                Name = randomString,
                Code = randomString
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result.Success);

        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000002")]
        [TestCase("00000000-0000-0000-0000-000000000003")]
        [TestCase("00000000-0000-0000-0000-000000000004")]
        [TestCase("00000000-0000-0000-0000-000000000005")]
        public void Handler_Error_Update(string guid)
        {
            var handler = new UpdateCategoryOfProductHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new UpdateCategoryOfProductCommand
            {
                Id = new Guid(guid),
                Name = randomString,
                Code = randomString
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result.Success == false);

        }
    }
}