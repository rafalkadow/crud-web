using MediatR;
using Shared.Interfaces;

namespace Domain.Modules.Product.Queries
{
    [Serializable]
    public class GetProductQueryById : GetProductBaseFilter, IQuery, IRequest<GetProductResultById>
    {
        public GetProductQueryById(Guid Id)
        {
            this.Id = Id;
        }

        public GetProductQueryById(string Code)
        {
            this.Code = Code;
        }
    }
}