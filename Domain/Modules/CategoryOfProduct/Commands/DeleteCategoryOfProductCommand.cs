using Domain.Modules.Base.Commands;
using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.CategoryOfProduct.Commands
{
    [Serializable]
    public class DeleteCategoryOfProductCommand : BaseActionCommand, IRequest<OperationResult>, ICommand
    {
    }
}