﻿using AutoMapper;
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

namespace Web.Api.Controllers.Base
{
    [Area(BaseConsts.Base)]
    [Route(BaseConsts.Base + "/" + BaseConsts.Active)]
    public class BaseActiveController : BaseController
    {

        public BaseActiveController(
            IMediator mediator,
            IMapper mapper,
            IDbContext dbContext,
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            definitionModel = new DefinitionModel(OperationEnum.Delete, userAccessor);
        }

        [HttpPost]
        [Route(BaseConsts.Action)]
        [UserAuthorization(OperationType = OperationEnum.Active, ClaimValue = BaseConsts.Base)]
        public async Task<IActionResult> Action([FromForm] BaseActiveCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var operationResult = await mediator.Send(command);
            return this.SwitchResultJson(operationResult);
        }
    }
}