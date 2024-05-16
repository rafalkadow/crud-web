using Domain.Modules.Communication.Generics;
using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.Product.Commands
{
    [Serializable]
    public class DeleteProductCommand : IRequest<ServiceResponse<OperationResult>>, ICommand
    {
        public ICollection<Guid> GuidList { get; set; }
    }
}