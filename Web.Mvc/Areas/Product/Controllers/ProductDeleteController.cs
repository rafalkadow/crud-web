using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.Product.Consts;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Attributes;
using Web.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Domain.Modules.Product.Commands;
using Domain.Modules.Communication.Generics;
using Shared.Models;

namespace Web.Areas.Product.Controllers
{
    [Serializable]
    [Area(ProductConsts.ControllerName)]
    [Route(ProductConsts.Url + "/" + BaseConsts.Delete)]
    public class ProductDeleteController : BaseController
    {
        /// <summary>
        /// Constructor class.
        /// </summary>
        public ProductDeleteController(ILogger<ProductDeleteController> logger,
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
        [UserAuthorization(OperationType = OperationEnum.Delete, ClaimValue = ProductConsts.ControllerName)]
        public async Task<IActionResult> Action([FromForm] DeleteProductCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var response = await mediator.Send(command) as ServiceResponse<OperationResult>;
            return this.SwitchResultJson(response);
        }
    }
}