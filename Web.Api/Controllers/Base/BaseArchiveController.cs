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

namespace Web.Areas.Base.Controllers
{
    [Area(BaseConsts.Base)]
    [Route(BaseConsts.Base + "/" + BaseConsts.Archive)]
    public class BaseArchiveController : BaseController
    {

        public BaseArchiveController(
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
        [UserAuthorization(OperationType = OperationEnum.Archive, ClaimValue = BaseConsts.Base)]
        public async Task<IActionResult> Action([FromForm] BaseArchiveCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var operationResult = await mediator.Send(command);
            return this.SwitchResultJson(operationResult);
        }
    }
}