using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.Product.Commands
{
    [Serializable]
    public class UpdateProductCommand : BaseProductCommand, IRequest<OperationResult>, ICommand
    {
    }
}