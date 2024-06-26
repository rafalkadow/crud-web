﻿using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Product.Commands;
using Shared.Models;
using Application.Modules.Base.Seeder;
using Application.Modules.Product.Create;
using Application.Modules.CategoryOfProduct.Queries;
using Domain.Modules.CategoryOfProduct.Queries;
using Shared.Helpers;
using Domain.Modules.Communication.Generics;

namespace Application.Modules.Product.Seeder
{
    [Serializable]
    public class ProductSeederData : BaseSeederClass
    {
        public ProductSeederData(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<ServiceResponse<OperationResult>> Data()
        {
            logger.Info($"Data()");
            try
            {
                var getHandlerPaymentAll = new GetCategoryOfProductQueryAllHandler(DbContext, Mapper, UserAccessor);
                var categoryOfProductList = await getHandlerPaymentAll.Handle(new GetCategoryOfProductQueryAll(), CancellationToken.None);
                var commandHandlerProduct = new CreateProductHandler(DbContext, Mapper, UserAccessor);

                for (int i = 0; i < 100; i++)
                {
                    var random = new Random();
                    var generator = new RandomGenerator();

                    var item = new CreateProductCommand() { Name = $"Product{i + 1}", Code = $"Code{i + 1}", };
                    int categoryOfProductIndex = random.Next(maxValue: categoryOfProductList.Count);
                    item.CategoryOfProductId = categoryOfProductList[categoryOfProductIndex].Id;
                    var value = generator.RandomNumber(1, 1000);
                    item.Value = value;
                    
                    var dateList = generator.RandomDate(DateTime.Now.AddMonths(-1 * value), DateTime.Now.AddMonths(value), 2);
                    item.DateUtc = dateList[0];
                    item.DateTimeUtc = dateList[1];

                    var result = await commandHandlerProduct.Handle(item, CancellationToken.None);
                    if (!result.Success)
                        return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Data(ex='{ex.ToString()}')");
            }
			return new ServiceResponse<OperationResult>(new OperationResult(true));
        }
    }
}