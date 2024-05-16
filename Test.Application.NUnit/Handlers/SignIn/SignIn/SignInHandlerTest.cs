using Domain.Modules.SignIn.Commands;
using Shared.Helpers;
using Application.Modules.SignIn.Commands;
using Application.Modules.SignIn.Queries;
using Domain.Modules.SignIn.Queries;
using Test.Application.NUnit.Handlers.Base;

namespace Test.Application.NUnit.Handlers.SignIn.Create
{
    [TestFixture]
    public class SignInHandlerTest : BaseHandlerBase
    {

        public SignInHandlerTest()
            : base()
        {
        }

        [Test]
        [TestCase("00000000-0000-0000-0000-000000000001")]
        public async Task Handler_ReturnsSuccess_SigInAsync(string guid)
        {
            var handlerRead = new GetSignInQueryByIdHandler(_dbContext, _mapper, userAccessor);

            var itemRead = new GetSignInQueryById(new Guid(guid));
            var resultRead = await handlerRead.Handle(itemRead, CancellationToken.None);
            Assert.That(resultRead, Is.Not.Null);
            var handler = new SignInHandler(_dbContext, _mapper, userAccessor);
            var item = new SignInCommand
            {
                SignInEmail = resultRead.EmailSignIn,
                SignInPassword = "pass123",
            };
            var result = await handler.Handle(item, CancellationToken.None);
            Assert.That(result.Success);
        }

        [Test]
        public async Task Handler_ReturnsError_SigInAsync()
        {
            var handler = new SignInHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomString = generator.RandomString(6);

            var item = new SignInCommand
            {
                SignInEmail = randomString,
                SignInPassword = "pass123",
            };
            var result = await handler.Handle(item, CancellationToken.None);
            Assert.That(result.Success == false);
        }

    }
}