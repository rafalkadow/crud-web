using Application.Extensions;
using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.Product.Commands;
using Domain.Modules.Product.Consts;
using Domain.Modules.Product.Queries;
using Domain.Modules.Product.ViewModels;
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

namespace Web.Areas.Product.Controllers
{
    [Serializable]
    [Area(ProductConsts.ControllerName)]
    [Route(ProductConsts.Url + "/" + BaseConsts.Update)]
    public class ProductUpdateController : BaseController
    {
        /// <summary>
        /// Constructor class.
        /// </summary>
        public ProductUpdateController(
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
        [UserAuthorization(OperationType = OperationEnum.Update, ClaimValue = ProductConsts.ControllerName)]
        public async Task<IActionResult> Index([FromRoute] Guid guid)
        {
            logger.Info($"Index(guid='{guid}')");
            var model = new ProductViewModel(definitionModel);
            var findElement = (GetProductResultById?) await  mediator.Send(new GetProductQueryById(guid));
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
        [UserAuthorization(OperationType = OperationEnum.Update, ClaimValue = ProductConsts.ControllerName)]
        public async Task<IActionResult> Action([FromForm] UpdateProductCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var operationResult = await mediator.Send(command);
            return this.SwitchResultJson(operationResult);
        }
    }
}