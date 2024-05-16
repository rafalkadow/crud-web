using Domain.Modules.Communication.Generics;
using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.Base.Commands
{
    [Serializable]
    public class BaseFirstCommand : BaseActionCommand, IRequest<ServiceResponse<OperationResult>>, ICommand
    {
        public ulong OrderId { get; set; }
    }
}