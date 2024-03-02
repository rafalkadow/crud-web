using Application.Modules.SignIn.Queries;
using Domain.Modules.SignIn.Queries;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.SignIn.Queries
{
    [TestFixture]
    public class SignInHandlerTestQueries : BaseHandlerBase
    {

        public SignInHandlerTestQueries()
        : base()
        {
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public async Task Handler_ReturnsSuccess_GetIdAsync(string guid)
        {
            var handler = new GetSignInQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetSignInQueryById(new Guid(guid));

            var result = await handler.Handle(item, CancellationToken.None);
            Assert.That(result.Id == new Guid(guid));
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000002")]
        [TestCase("00000000-0000-0000-0000-000000000003")]
        [TestCase("00000000-0000-0000-0000-000000000004")]
        [TestCase("00000000-0000-0000-0000-000000000005")]
        public async Task Handler_ReturnsError_GetIdAsync(string guid)
        {
            var handler = new GetSignInQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var item = new GetSignInQueryById(new Guid(guid));

            var result = await handler.Handle(item, CancellationToken.None);
            Assert.That(result == null);
        }
    }
}