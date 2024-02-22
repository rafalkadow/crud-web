using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Product.Models;
using Domain.Modules.Product.Queries;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Application.Modules.Base.Queries;

namespace Application.Modules.Product.Queries
{
    [Serializable]
	public class GetProductQueryByIdHandler : BaseQueryHandler, IRequestHandler<GetProductQueryById, GetProductResultById>
	{
		public GetProductQueryByIdHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
			: base(dbContext, mapper, userAccessor)
		{
		}

		public async Task<GetProductResultById> Handle(GetProductQueryById filter, CancellationToken cancellationToken)
		{
			logger.Debug($"Handle(filter='{filter.RenderProperties()}', cancellationToken='{cancellationToken}')");
            GetProductResultById? model = null;
			try
			{
				ProductModel? modelDb = null;
				var queryable = DbContext.GetQueryable<ProductModel>().AsNoTracking();
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
                    model = Mapper.Map<GetProductResultById>(modelDb);

                return model;
            }
			catch (Exception ex)
			{
				logger.Error($"Handle(ex='{ex.ToString()}')");
                throw;
            }
		}

    }
}