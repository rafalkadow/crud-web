﻿using Application.Extensions;
using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.CategoryOfProduct.Commands;
using Domain.Modules.CategoryOfProduct.Consts;
using Domain.Modules.CategoryOfProduct.ViewModels;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Attributes;
using Web.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Shared.Models;

namespace Web.Areas.CategoryOfProduct.Controllers
{
    [Serializable]
    [Area(CategoryOfProductConsts.ControllerName)]
    [Route(CategoryOfProductConsts.Url + "/" + BaseConsts.Create)]
    public class CategoryOfProductCreateController : BaseController
    {
        
        public CategoryOfProductCreateController(
            IMediator mediator, 
            IMapper mapper,
            IDbContext dbContext, 
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);
        }

        [HttpGet]
        [UserAuthorization(OperationType = OperationEnum.Create, ClaimValue = CategoryOfProductConsts.ControllerName)]
        public IActionResult Index()
        {
            logger.Info($"Index()");
            var model = new CategoryOfProductViewModel(definitionModel);
            return View(model.ViewName, model);
        }

        [HttpPost]
        [Route(BaseConsts.Action)]
        [ValidateAntiForgeryToken]
        [UserAuthorization(OperationType = OperationEnum.Create, ClaimValue = CategoryOfProductConsts.ControllerName)]
        public async Task<IActionResult> Action([FromForm] CreateCategoryOfProductCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var operationResult = await mediator.Send(command);
            return this.SwitchResultJson(operationResult);
        }
    }
}