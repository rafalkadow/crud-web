using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Product.Models;
using Domain.Modules.Product.Queries;
using Microsoft.EntityFrameworkCore;
using Domain.Modules.Base.Extensions;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Application.Modules.Base.Queries;
using Domain.Modules.CategoryOfProduct.Models;

namespace Application.Modules.Product.Queries
{
    [Serializable]
    public class GetProductQueryAllHandler : BaseQueryHandler, IRequestHandler<GetProductQueryAll, IEnumerable<GetProductResultAll>>
    {
        public GetProductQueryAllHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<IEnumerable<GetProductResultAll>> Handle(GetProductQueryAll filter, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(filter='{filter.RenderProperties()}', cancellationToken='{cancellationToken}')");
            try
            {
                var list = DbContext.GetQueryable<ProductModel>().Include(x => x.CategoryOfProduct).AsNoTracking();

                var query = list.Where(x =>
                                (string.IsNullOrEmpty(filter.Name) || (filter.CaseSensitiveComparison ? x.Name.Contains(filter.Name) : x.Name.ToUpper().Contains(filter.Name.ToUpper()))) &&
                                (string.IsNullOrEmpty(filter.Code) || (filter.CaseSensitiveComparison ? x.Code.Contains(filter.Code) : x.Code.ToUpper().Contains(filter.Code.ToUpper()))) &&
                                //(string.IsNullOrEmpty(filter.CategoryOfProduct.Name) || (filter.CaseSensitiveComparison ? x.CategoryOfProduct.Name.Contains(filter.CategoryOfProduct.Name) : x.CategoryOfProduct.Name.ToUpper().Contains(filter.CategoryOfProduct.Name.ToUpper()))) &&
                                (filter.CreatedFrom == null || x.CreatedOnDateTimeUTC >= filter.CreatedFrom.Value.ToUniversalTime()) &&
                                (filter.CreatedTo == null || x.CreatedOnDateTimeUTC <= filter.CreatedTo.Value.ToUniversalTime())
                           );

                filter.TotalRecords = query.Count();

                list = query.OrderByTypeSort(y => y.OrderId, filter.OrderSortValue).Skip(filter.DisplayStart);

                if (filter.DisplayLengthActive)
                    list = list.Take(filter.DisplayLength);

                var selected = await list.AsNoTracking().ToListAsync();
                //UTC
                var selectedList = selected.Select(x =>
                {
                    return Mapper.Map<GetProductResultAll>(x);
                }).ToList();

                return selectedList;
            }
            catch (Exception ex)
            {
                logger.Error($"Handle(ex='{ex.ToString()}')");
                throw;
            }
        }
    }
}