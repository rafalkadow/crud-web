using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.CategoryOfProduct.Models;
using Domain.Modules.CategoryOfProduct.Queries;
using Microsoft.EntityFrameworkCore;
using Domain.Modules.Base.Extensions;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Application.Modules.Base.Queries;
using Shared.Enums;

namespace Application.Modules.CategoryOfProduct.Queries
{
    [Serializable]
    public class GetCategoryOfProductQueryAllHandler : BaseQueryHandler, IRequestHandler<GetCategoryOfProductQueryAll, IList<GetCategoryOfProductResultAll>>
    {
        public GetCategoryOfProductQueryAllHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<IList<GetCategoryOfProductResultAll>> Handle(GetCategoryOfProductQueryAll filter, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(filter='{filter.RenderProperties()}', cancellationToken='{cancellationToken}')");
            try
            {
                var list = DbContext.GetQueryable<CategoryOfProductModel>().AsNoTracking();

                var query = list.Where(x =>
                                (string.IsNullOrEmpty(filter.Name) || (filter.CaseSensitiveComparison ? x.Name.Contains(filter.Name) : x.Name.ToUpper().Contains(filter.Name.ToUpper()))) &&
                                (string.IsNullOrEmpty(filter.Code) || (filter.CaseSensitiveComparison ? x.Code.Contains(filter.Code) : x.Code.ToUpper().Contains(filter.Code.ToUpper()))) &&
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
                    return Mapper.Map<GetCategoryOfProductResultAll>(x);
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