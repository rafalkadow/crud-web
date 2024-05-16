using AutoMapper;
using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.SignIn.Commands;
using Domain.Modules.SignIn.Consts;
using Domain.Modules.SignIn.ViewModels;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Areas.Base.Redirects;
using Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Domain.Modules.Communication.Generics;
using Shared.Models;

namespace Web.Areas.SignIn.Controllers
{
    [Serializable]
    [Area(SignInConsts.ControllerName)]
    [Route(SignInConsts.Url)]
    public class SignInViewController : BaseController
    {
        /// <summary>
        /// Constructor class.
        /// </summary>
        public SignInViewController(
                    IMediator mediator, 
                    IMapper mapper,
                    IDbContext dbContext, 
                    IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.View, userAccessor);
        }

        [HttpGet]
        public IActionResult Index([FromQuery(Name = "ReturnUrl")] string? ReturnUrl = null)
        {
            logger.Info($"Index(ReturnUrl = '{ReturnUrl}')");

            var model = new SignInViewModel(definitionModel);
            if (model.LanguageRedirect)
            {
                return this.RedirectToLanguage(model, model.LanguageCulture);
            }
            model.ReturnUrl = ReturnUrl;
            return View(model.ViewName, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(BaseConsts.View + "/" + BaseConsts.Action)]
        public async Task<IActionResult> Action([FromForm] SignInCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var response = await mediator.Send(command) as ServiceResponse<OperationResult>;
            return this.SignInUserToApplication(command, response);
        }
    }
}