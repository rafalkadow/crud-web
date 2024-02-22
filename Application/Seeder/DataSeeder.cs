﻿using AutoMapper;
using Domain.Interfaces;
using System.Diagnostics;
using Shared.Models;
using Domain.Modules.Account;
using Application.Modules.Account.Seeder;
using Shared.Enums;
using Application.Modules.CategoryOfProduct.Seeder;
using NLog;
using Application.Modules.Product.Seeder;

namespace Application.Seeder
{
    public class DataSeeder
	{
		public async Task<OperationResult> SeedDataOnApplication(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor, ILogger logger)
		{
			logger.Info($"SeedDataOnApplication()");
			try
			{
				if (!dbContext.GetQueryable<AccountModel>().Any())
				{
					var watch = Stopwatch.StartNew();

					await new AccountSeederData(dbContext, mapper, userAccessor).PrimaryUser();

                    await new CategoryOfProductSeederData(dbContext, mapper, userAccessor).Data();
                    await new ProductSeederData(dbContext, mapper, userAccessor).Data();

                    await new AccountSeederData(dbContext, mapper, userAccessor).Data();

					//ToAddData
					watch.Stop();
					logger.Info($"SeedDataOnApplication : Execution Time: {watch.ElapsedMilliseconds} ms");
				}
			}
			catch (Exception ex)
			{
				logger.Error($"SeedDataOnApplication(ex='{ex.ToString()}')");
				throw;
			}
			return new OperationResult(true, "", OperationEnum.Create);
		}
	}
}