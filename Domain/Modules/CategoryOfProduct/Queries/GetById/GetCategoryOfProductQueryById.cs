using MediatR;
using Shared.Interfaces;

namespace Domain.Modules.CategoryOfProduct.Queries
{
    [Serializable]
    public class GetCategoryOfProductQueryById : GetCategoryOfProductBaseFilter, IQuery, IRequest<GetCategoryOfProductResultById>
    {
        public GetCategoryOfProductQueryById(Guid Id)
        {
            this.Id = Id;
        }

        public GetCategoryOfProductQueryById(string Code)
        {
            this.Code = Code;
        }
    }
}