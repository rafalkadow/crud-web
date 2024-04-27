using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Product.Models;
using Domain.Modules.Product.Queries;
using Microsoft.EntityFrameworkCore;
using Domain.Modules.Base.Extensions;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Application.Modules.Base.Queries;
using Shared.Enums;

namespace Application.Modules.Product.Queries
{
    [Serializable]
    public class GetProductQueryAllHandler : BaseQueryHandler, IRequestHandler<GetProductQueryAll, IList<GetProductResultAll>>
    {
        public GetProductQueryAllHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<IList<GetProductResultAll>> Handle(GetProductQueryAll filter, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(filter='{filter.RenderProperties()}', cancellationToken='{cancellationToken}')");
            try
            {
                var list = DbContext.GetQueryable<ProductModel>().Include(x => x.CategoryOfProduct).AsNoTracking();

                var query = list.Where(x =>
                                (string.IsNullOrEmpty(filter.Name) || (filter.CaseSensitiveComparison ? x.Name.Contains(filter.Name) : x.Name.ToUpper().Contains(filter.Name.ToUpper()))) &&
                                (string.IsNullOrEmpty(filter.Code) || (filter.CaseSensitiveComparison ? x.Code.Contains(filter.Code) : x.Code.ToUpper().Contains(filter.Code.ToUpper()))) &&
                                (string.IsNullOrEmpty(filter.CategoryOfProductName) || (filter.CaseSensitiveComparison ? x.CategoryOfProduct.Name.Contains(filter.CategoryOfProductName) : x.CategoryOfProduct.Name.ToUpper().Contains(filter.CategoryOfProductName.ToUpper()))) &&
                                (filter.RecordStatus == RecordStatusEnum.AllRecords || x.RecordStatus == filter.RecordStatus) &&
                                (filter.CreatedFrom == null || x.CreatedOnDateTimeUTC >= filter.CreatedFrom.Value.ToUniversalTime()) &&
                                (filter.CreatedTo == null || x.CreatedOnDateTimeUTC <= filter.CreatedTo.Value.ToUniversalTime()));

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