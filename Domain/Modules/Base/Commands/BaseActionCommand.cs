using Domain.Modules.Communication.Generics;
using MediatR;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.Modules.Base.Commands
{
    [Serializable]
    public class BaseActionCommand : IRequest<ServiceResponse<OperationResult>>, ICommand
    {
        public ICollection<Guid> GuidList { get; set; }

        public string ControllerName { get; set; }

    }
}