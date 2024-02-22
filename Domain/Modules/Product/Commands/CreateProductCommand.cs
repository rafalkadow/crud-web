using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.Product.Commands
{
    [Serializable]
    public class CreateProductCommand : BaseProductCommand, ICommand, IRequest<OperationResult>
    {
    }
}