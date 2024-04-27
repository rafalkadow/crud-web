using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Account.Queries;
using Domain.Modules.Base.Models;
using Domain.Modules.SignOut.Consts;
using Domain.Modules.SignOut.ViewModels;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Web.Areas.SignOut.Controllers
{
    [Serializable]
    [Area(SignOutConsts.ControllerName)]
    [Route(SignOutConsts.Url)]
    public class SignOutViewController : BaseController
    {
        /// <summary>
        /// Constructor class.
        /// </summary>
        public SignOutViewController(
            IMediator mediator,
            IMapper mapper, 
            IDbContext dbContext,
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.View, userAccessor);
        }

        [HttpGet]
        [Route("{guid}")]
        public async Task<IActionResult> Index([FromRoute] Guid guid)
        {
            logger.Info($"Index(guid='{guid}')");
            var findElement = (GetAccountResultById?) await mediator.Send(new GetAccountQueryById(guid));
            var model = new SignOutViewModel(definitionModel)
            {
                EmailSignOut = findElement != null ? findElement.AccountEmail : String.Empty,
            };
            this.SignOutUserToApplication();
            return View(model.ViewName, model);
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            logger.Info($"Index()");
            string Email = this.SignOutUserToApplication();
            var model = new SignOutViewModel(definitionModel)
            {
                EmailSignOut = Email,
            };
            return View(model.ViewName, model);
        }
    }
}