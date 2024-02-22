using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.CategoryOfProduct.Commands
{
    [Serializable]
    public class UpdateCategoryOfProductCommand : BaseCategoryOfProductCommand, IRequest<OperationResult>, ICommand
    {
    }
}