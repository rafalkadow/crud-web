using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.Product.Commands;
using Domain.Modules.Product.Consts;
using Domain.Modules.Product.ViewModels;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Attributes;
using Web.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;

namespace Web.Areas.Product.Controllers
{
    [Serializable]
    [Area(ProductConsts.ControllerName)]
    [Route(ProductConsts.Url + "/" + BaseConsts.Create)]
    public class ProductCreateController : BaseController
    {
        public ProductCreateController(
            IMediator mediator, 
            IMapper mapper,
            IDbContext dbContext, 
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.Create, userAccessor);
        }

        [HttpGet]
        [UserAuthorization(OperationType = OperationEnum.Create, ClaimValue = ProductConsts.ControllerName)]
        public IActionResult Index()
        {
            logger.Info($"Index()");
            var model = new ProductViewModel(definitionModel);
            return View(model.ViewName, model);
        }

        [HttpPost]
        [Route(BaseConsts.Action)]
        [ValidateAntiForgeryToken]
        [UserAuthorization(OperationType = OperationEnum.Create, ClaimValue = ProductConsts.ControllerName)]
        public async Task<IActionResult> Action([FromForm] CreateProductCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var operationResult = await mediator.Send(command);
            return this.SwitchResultJson(operationResult);
        }
    }
}