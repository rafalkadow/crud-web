using Domain.Modules.Base.Commands;
using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.Product.Commands
{
    [Serializable]
    public class DeleteProductCommand : BaseActionCommand, IRequest<OperationResult>, ICommand
    {
    }
}