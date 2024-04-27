using Domain.Interfaces;
using Domain.Modules.Base.Models;
using Domain.Modules.Product.Consts;
using Domain.Modules.Product.ViewModels;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Web.Areas.Product.Controllers
{
    [Serializable]
    [Area(ProductConsts.ControllerName)]
    [Route(ProductConsts.Url)]
    public class ProductViewController : BaseController
    {
        /// <summary>
        /// Constructor class.
        /// </summary>
        public ProductViewController(
            IMediator mediator,
            IMapper mapper,
            IDbContext dbContext,
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.View, userAccessor);
        }

        [HttpGet]
        [Route("~/", Name = "default")]
        [Route("")]
        [UserAuthorization(OperationType = OperationEnum.View, ClaimValue = ProductConsts.ControllerName)]
        public IActionResult Index()
        {
            logger.Info($"Index()");
            var model = new ProductViewModel(definitionModel);
            return View(model.ViewName, model);
        }
    }
}