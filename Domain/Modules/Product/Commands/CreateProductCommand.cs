using Domain.Modules.Communication.Generics;
using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.Product.Commands
{
    [Serializable]
    public class CreateProductCommand : BaseProductCommand, ICommand, IRequest<ServiceResponse<OperationResult>>
    {
    }
}