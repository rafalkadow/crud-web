using Domain.Modules.Communication.Generics;
using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.CategoryOfProduct.Commands
{
    [Serializable]
    public class UpdateCategoryOfProductCommand : BaseCategoryOfProductCommand, IRequest<ServiceResponse<OperationResult>>, ICommand
    {
    }
}