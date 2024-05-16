using Domain.Modules.Communication.Generics;
using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.CategoryOfProduct.Commands
{
    [Serializable]
    public class CreateCategoryOfProductCommand : BaseCategoryOfProductCommand, ICommand, IRequest<ServiceResponse<OperationResult>>
    {
    }
}