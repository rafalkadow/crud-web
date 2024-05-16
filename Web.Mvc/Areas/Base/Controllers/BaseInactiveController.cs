using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Base.Commands;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Shared.Enums;
using Web.Attributes;
using Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Domain.Modules.Communication.Generics;
using Shared.Models;

namespace Web.Areas.Base.Controllers
{
    [Area(BaseConsts.Base)]
    [Route(BaseConsts.Base + "/" + BaseConsts.Inactive)]
    public class BaseInactiveController : BaseController
    {

        public BaseInactiveController(
            IMediator mediator, 
            IMapper mapper,
            IDbContext dbContext, 
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.Delete, userAccessor);
        }

        [HttpPost]
        [Route(BaseConsts.Action)]
        [UserAuthorization(OperationType = OperationEnum.Inactive, ClaimValue = BaseConsts.Base)]
        public async Task<IActionResult> Action([FromForm] BaseInactiveCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var response = await mediator.Send(command) as ServiceResponse<OperationResult>;
            return this.SwitchResultJson(response);
        }
    }
}