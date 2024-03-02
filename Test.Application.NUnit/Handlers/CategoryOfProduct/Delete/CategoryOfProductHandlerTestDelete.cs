using Domain.Modules.CategoryOfProduct.Commands;
using Application.Modules.CategoryOfProduct.Delete;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.CategoryOfProduct.Delete
{
    [TestFixture]
    public class CategoryOfProductHandlerTestDelete : BaseHandlerBase
    {

        public CategoryOfProductHandlerTestDelete()
        : base()
        {
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public void Handler_Success_Delete(string guid)
        {
            var handler = new DeleteCategoryOfProductHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteCategoryOfProductCommand
            {
                GuidList = new List<Guid> { new Guid(guid) },
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result.OperationStatus);
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000002")]
        [TestCase("00000000-0000-0000-0000-000000000003")]
        [TestCase("00000000-0000-0000-0000-000000000004")]
        [TestCase("00000000-0000-0000-0000-000000000005")]
        public void Handler_Error_Delete(string guid)
        {
            var handler = new DeleteCategoryOfProductHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteCategoryOfProductCommand
            {
                GuidList = new List<Guid> { new Guid(guid) },
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result.OperationStatus == false);
        }
    }
}