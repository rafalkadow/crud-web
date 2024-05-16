using Domain.Modules.Communication.Generics;
using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.CategoryOfProduct.Commands
{
    [Serializable]
    public class DeleteCategoryOfProductCommand : IRequest<ServiceResponse<OperationResult>>, ICommand
    {
        public ICollection<Guid> GuidList { get; set; }
    }
}