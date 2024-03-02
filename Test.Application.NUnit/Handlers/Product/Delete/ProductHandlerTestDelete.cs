using Domain.Modules.Product.Commands;
using Application.Modules.Product.Delete;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.Product.Delete
{
    [TestFixture]
    public class ProductHandlerTestDelete : BaseHandlerBase
    {

        public ProductHandlerTestDelete()
        : base()
        {
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public void Handler_Success_Delete(string guid)
        {
            var handler = new DeleteProductHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteProductCommand
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
            var handler = new DeleteProductHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteProductCommand
            {
                GuidList = new List<Guid> { new Guid(guid) },
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result.OperationStatus == false);
        }
    }
}