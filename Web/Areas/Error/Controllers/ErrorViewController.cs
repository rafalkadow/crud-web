using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.Error.Consts;
using Domain.Modules.Error.Queries;
using Domain.Modules.Error.ViewModels;
using Web.Areas.Base.Controllers;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Shared.Enums;

namespace Web.Areas.Error.Controllers
{
    [Serializable]
    [Area(ErrorConsts.ControllerName)]
    [Route(ErrorConsts.Url)]
    public class ErrorViewController : BaseController
    {
        public ErrorViewController(IMediator mediator,
            IMapper mapper,
            IDbContext dbContext,
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.View, userAccessor);
        }

        [HttpGet]
        [Route("{errorCode?}")]
        public async Task<IActionResult> Error([FromRoute] string? errorCode = null)
        {
            logger.Info($"Error(errorCode='{errorCode}')");
            var model = new ErrorViewModel(definitionModel);
            var findElement = await mediator.Send(new GetErrorQueryById() { ErrorCode = errorCode });
            mapper.Map(findElement, model);
            return View(model.ViewName, model);
        }

        [HttpGet]
        [Route("{errorCode}/Url")]
        public async Task<IActionResult> ErrorWithUrl([FromRoute] string errorCode, [FromQuery(Name = "errorUrl")] string errorUrl = null)
        {
            logger.Info($"Error(errorCode='{errorCode}', errorUrl='{errorUrl}')");
            var model = new ErrorViewModel(definitionModel);
            var findElement = await mediator.Send(new GetErrorQueryById() { ErrorCode = errorCode, ErrorUrl = errorUrl });
            mapper.Map(findElement, model);
            return View(model.ViewName, model);
        }
    }
}