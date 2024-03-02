using Domain.Modules.Account.Commands;
using Application.Modules.Account.Delete;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Account.Delete
{
    [TestClass]
    public class AccountHandlerTestDelete : BaseHandlerBase
    {

        public AccountHandlerTestDelete()
        : base()
        {
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public void Handler_Success_Delete(string guid)
        {
            var handler = new DeleteAccountHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteAccountCommand
            {
                GuidList = new List<Guid> { new Guid(guid) },
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.OperationStatus);
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000002")]
        [DataRow("00000000-0000-0000-0000-000000000003")]
        [DataRow("00000000-0000-0000-0000-000000000004")]
        [DataRow("00000000-0000-0000-0000-000000000005")]
        public void Handler_Error_Delete(string guid)
        {
            var handler = new DeleteAccountHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteAccountCommand
            {
                GuidList = new List<Guid> { new Guid(guid) },
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsFalse(result.OperationStatus);
        }
    }
}