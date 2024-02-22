using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.CategoryOfProduct.Commands;
using Shared.Models;
using Application.Modules.Base.Seeder;
using Application.Modules.CategoryOfProduct.Create;
using NLog;

namespace Application.Modules.CategoryOfProduct.Seeder
{
    [Serializable]
    public class CategoryOfProductSeederData : BaseSeederClass
    {
        public CategoryOfProductSeederData(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<OperationResult> Data()
        {
            logger.Info($"Data()");
            try
            {
                var commandHandlerCategoryOfProduct = new CreateCategoryOfProductHandler(DbContext, Mapper, UserAccessor);

                for (int i = 0; i < 100; i++)
                {
                    var item = new CreateCategoryOfProductCommand() { Name = $"CategoryOfProduct{i + 1}", Code = $"Code{i + 1}", };
                    var result = await commandHandlerCategoryOfProduct.Handle(item, CancellationToken.None);
                    if (!result.OperationStatus)
                        return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Data(ex='{ex.ToString()}')");
            }
			return new OperationResult(true);
		}
    }
}