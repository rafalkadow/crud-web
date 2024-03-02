using Application.Modules.SignIn.Queries;
using Domain.Modules.SignIn.Queries;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.SignIn.Queries
{
    [TestClass]
    public class SignInHandlerTestQueries : BaseHandlerBase
    {

        public SignInHandlerTestQueries()
        : base()
        {
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public async Task Handler_ReturnsSuccess_GetIdAsync(string guid)
        {
            var handler = new GetSignInQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetSignInQueryById(new Guid(guid));

            var result = await handler.Handle(item, CancellationToken.None);
            Assert.IsTrue(result.Id == new Guid(guid));
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000002")]
        [DataRow("00000000-0000-0000-0000-000000000003")]
        [DataRow("00000000-0000-0000-0000-000000000004")]
        [DataRow("00000000-0000-0000-0000-000000000005")]
        public async Task Handler_ReturnsError_GetIdAsync(string guid)
        {
            var handler = new GetSignInQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetSignInQueryById(new Guid(guid));

            var result = await handler.Handle(item, CancellationToken.None);
            Assert.IsTrue(result == null);
        }
    }
}