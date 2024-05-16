using Domain.Modules.Communication.Generics;
using MediatR;
using Shared.Enums;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.Base.Commands
{
    [Serializable]
    public class BaseActiveCommand : BaseActionCommand, IRequest<ServiceResponse<OperationResult>>, ICommand
    {
        public RecordStatusEnum RecordStatus { get; set; }
    }
}