﻿using Domain.Modules.Product.Commands;
using Shared.Helpers;
using Application.Modules.Product.Update;
using Test.Application.MSTest.Handlers.Base;

namespace Test.Application.MSTest.Handlers.Product.Update
{
    [TestClass]
    public class ProductHandlerTestUpdate : BaseHandlerBase
    {

        public ProductHandlerTestUpdate()
        : base()
        {
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000001")]
        public void Handler_ReturnsSuccess_Update(string guid)
        {
            var handler = new UpdateProductHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new UpdateProductCommand
            {
                Id = new Guid(guid),
                Name = randomString,
                Code = randomString
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsTrue(result.Success);

        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000002")]
        [DataRow("00000000-0000-0000-0000-000000000003")]
        [DataRow("00000000-0000-0000-0000-000000000004")]
        [DataRow("00000000-0000-0000-0000-000000000005")]
        public void Handler_Error_Update(string guid)
        {
            var handler = new UpdateProductHandler(_dbContext, _mapper, userAccessor);
            var generator = new RandomGenerator();
            var randomNumber = generator.RandomNumber(5, 100);
            var randomString = generator.RandomString(3);

            var item = new UpdateProductCommand
            {
                Id = new Guid(guid),
                Name = randomString,
                Code = randomString
            };

            var result = handler.Handle(item, CancellationToken.None).Result;
            Assert.IsFalse(result.Success);

        }
    }
}