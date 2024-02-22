﻿using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.CategoryOfProduct.Consts;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Attributes;
using Web.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Domain.Modules.CategoryOfProduct.Commands;
using NLog;

namespace Web.Areas.CategoryOfProduct.Controllers
{
    [Serializable]
    [Area(CategoryOfProductConsts.ControllerName)]
    [Route(CategoryOfProductConsts.Url + "/" + BaseConsts.Delete)]
    public class CategoryOfProductDeleteController : BaseController
    {
        
        public CategoryOfProductDeleteController(
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
        [ValidateAntiForgeryToken]
        [UserAuthorization(OperationType = OperationEnum.Delete, ClaimValue = CategoryOfProductConsts.ControllerName)]
        public async Task<IActionResult> Action([FromForm] DeleteCategoryOfProductCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var operationResult = await mediator.Send(command);
            return this.SwitchResultJson(operationResult);
        }
    }
}