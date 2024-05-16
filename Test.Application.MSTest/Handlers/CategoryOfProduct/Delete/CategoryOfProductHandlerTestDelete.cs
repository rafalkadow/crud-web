using Domain.Modules.CategoryOfProduct.Commands;
using Application.Modules.CategoryOfProduct.Delete;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.CategoryOfProduct.Delete
{
    [TestClass]
    public class CategoryOfProductHandlerTestDelete : BaseHandlerBase
    {

        public CategoryOfProductHandlerTestDelete()
        : base()
        {
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public void Handler_Success_Delete(string guid)
        {
            var handler = new DeleteCategoryOfProductHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteCategoryOfProductCommand
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
            var handler = new DeleteCategoryOfProductHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteCategoryOfProductCommand
            {
                GuidList = new List<Guid> { new Guid(guid) },
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsFalse(result.Success);
        }
    }
}