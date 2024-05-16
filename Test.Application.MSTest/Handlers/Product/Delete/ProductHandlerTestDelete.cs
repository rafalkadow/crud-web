using Domain.Modules.Product.Commands;
using Application.Modules.Product.Delete;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Product.Delete
{
    [TestClass]
    public class ProductHandlerTestDelete : BaseHandlerBase
    {

        public ProductHandlerTestDelete()
        : base()
        {
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public void Handler_Success_Delete(string guid)
        {
            var handler = new DeleteProductHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteProductCommand
            {
                GuidList = new List<Guid> { new Guid(guid) },
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000002")]
        [DataRow("00000000-0000-0000-0000-000000000003")]
        [DataRow("00000000-0000-0000-0000-000000000004")]
        [DataRow("00000000-0000-0000-0000-000000000005")]
        public void Handler_Error_Delete(string guid)
        {
            var handler = new DeleteProductHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteProductCommand
            {
                GuidList = new List<Guid> { new Guid(guid) },
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsFalse(result.Success);
        }
    }
}