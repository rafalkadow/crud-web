using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Base.Commands;
using Domain.Modules.Base.Models;
using Domain.Modules.Communication.Generics;
using MediatR;
using Shared.Enums;
using Shared.Extensions.GeneralExtensions;
using Shared.Models;

namespace Application.Modules.Base.Commands
{
    [Serializable]
    public class BaseInactiveCommandHandler : BaseCommandHandler, IRequestHandler<BaseInactiveCommand, ServiceResponse<OperationResult>>
    {
        public BaseInactiveCommandHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
            : base(dbContext, mapper, userAccessor)
        {
        }

        public async Task<ServiceResponse<OperationResult>> Handle(BaseInactiveCommand command, CancellationToken cancellationToken)
        {
            logger.Info($"Handle(command='{command.RenderProperties()}')");

            try
            {
                if (command.GuidList == null || !command.GuidList.Any())
                    return new ServiceResponse<OperationResult>(new OperationResult(false) { ErrorMessage = "There is no data in the list Id." });

                foreach (var guidId in command.GuidList)
                {
                    var operation = new OperationModel
                    {
                        Id = guidId,
                        Operation = OperationEnum.Inactive,
                        ControllerName = command.ControllerName,
                    };

                    var result = await CommandChoiceAsync(operation);
                    if (!result.Success)
                    {
                        return result;
                    }
                }

                return new ServiceResponse<OperationResult>(new OperationResult(true, OperationEnum.Inactive));
            }
            catch (Exception ex)
            {
                logger.Error($"Handle(ex='{ex.ToString()}')");
                throw;
            }
        }

    }
}