using Domain.Modules.Account.Commands;
using Application.Modules.Account.Delete;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.Account.Delete
{
    [TestFixture]
    public class AccountHandlerTestDelete : BaseHandlerBase
    {

        public AccountHandlerTestDelete()
        : base()
        {
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public void Handler_Success_Delete(string guid)
        {
            var handler = new DeleteAccountHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteAccountCommand
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
            var handler = new DeleteAccountHandler(_dbContext, _mapper, userAccessor);

            var item = new DeleteAccountCommand
            {
                GuidList = new List<Guid> { new Guid(guid) },
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.That(result.OperationStatus == false);
        }
    }
}