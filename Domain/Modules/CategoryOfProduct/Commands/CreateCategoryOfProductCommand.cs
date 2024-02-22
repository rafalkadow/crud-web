using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.CategoryOfProduct.Commands
{
    [Serializable]
    public class CreateCategoryOfProductCommand : BaseCategoryOfProductCommand, ICommand, IRequest<OperationResult>
    {
    }
}