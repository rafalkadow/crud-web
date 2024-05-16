using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.CategoryOfProduct.Models;
using Domain.Modules.CategoryOfProduct.Queries;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Application.Modules.Base.Queries;
using Domain.Modules.Communication.Generics;

namespace Application.Modules.CategoryOfProduct.Queries
{
    [Serializable]
	public class GetCategoryOfProductQueryByIdHandler : BaseQueryHandler, IRequestHandler<GetCategoryOfProductQueryById, ServiceResponse<GetCategoryOfProductResultById>>
	{
		public GetCategoryOfProductQueryByIdHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
			: base(dbContext, mapper, userAccessor)
		{
		}

		public async Task<ServiceResponse<GetCategoryOfProductResultById>> Handle(GetCategoryOfProductQueryById filter, CancellationToken cancellationToken)
		{
			logger.Debug($"Handle(filter='{filter.RenderProperties()}', cancellationToken='{cancellationToken}')");
            GetCategoryOfProductResultById? model = null;
			try
			{
				CategoryOfProductModel? modelDb = null;
                var queryable = DbContext.GetQueryable<CategoryOfProductModel>().AsNoTracking();
                if (filter.Id != Guid.Empty)
				{
					modelDb = await queryable
                       .FirstOrDefaultAsync(x => x.Id == filter.Id);
				}
				else if (!string.IsNullOrEmpty(filter.Name))
				{
					modelDb = await queryable
					   .FirstOrDefaultAsync(x => x.Name == filter.Name);
				}
                else if (!string.IsNullOrEmpty(filter.Code))
                {
                    modelDb = await queryable
                       .FirstOrDefaultAsync(x => x.Code == filter.Code);
                }

                if (modelDb != null)
                    model = Mapper.Map<GetCategoryOfProductResultById>(modelDb);

                return new ServiceResponse<GetCategoryOfProductResultById>(model);
            }
			catch (Exception ex)
			{
				logger.Error($"Handle(ex='{ex.ToString()}')");
                throw;
            }
		}

    }
}