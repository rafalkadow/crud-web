using Application.Extensions;
using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.CategoryOfProduct.Commands;
using Domain.Modules.CategoryOfProduct.Consts;
using Domain.Modules.CategoryOfProduct.Queries;
using Domain.Modules.CategoryOfProduct.ViewModels;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Areas.Base.Redirects;
using Web.Attributes;
using Web.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;

namespace Web.Areas.CategoryOfProduct.Controllers
{
    [Serializable]
    [Area(CategoryOfProductConsts.ControllerName)]
    [Route(CategoryOfProductConsts.Url + "/" + BaseConsts.Update)]
    public class CategoryOfProductUpdateController : BaseController
    {
        /// <summary>
        /// Constructor class.
        /// </summary>
        public CategoryOfProductUpdateController(
            IMediator mediator,
            IMapper mapper,
            IDbContext dbContext,
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.Update, userAccessor);
        }

        [HttpGet]
        [Route("{guid}")]
        [UserAuthorization(OperationType = OperationEnum.Update, ClaimValue = CategoryOfProductConsts.ControllerName)]
        public async Task<IActionResult> Index([FromRoute] Guid guid)
        {
            logger.Info($"Index(guid='{guid}')");
            var model = new CategoryOfProductViewModel(definitionModel);
            var findElement = (GetCategoryOfProductResultById?) await  mediator.Send(new GetCategoryOfProductQueryById(guid));
            if (findElement == null || findElement.Id == Guid.Empty)
            {
                return this.Redirect404(this.Request.GetDisplayUrl());
            }
            mapper.Map(findElement, model);
            return View(model.ViewName, model);
        }

        [HttpPost]
        [Route(BaseConsts.Action)]
        [ValidateAntiForgeryToken]
        [UserAuthorization(OperationType = OperationEnum.Update, ClaimValue = CategoryOfProductConsts.ControllerName)]
        public async Task<IActionResult> Action([FromForm] UpdateCategoryOfProductCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var operationResult = await mediator.Send(command);
            return this.SwitchResultJson(operationResult);
        }
    }
}