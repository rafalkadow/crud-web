using MediatR;
using Shared.Interfaces;

namespace Domain.Modules.CategoryOfProduct.Queries
{
    [Serializable]
    public class GetCategoryOfProductQueryAll : GetCategoryOfProductBaseFilter, IRequest<IList<GetCategoryOfProductResultAll>>, IQuery
    {
    }
}