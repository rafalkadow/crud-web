using Domain.Interfaces;
using Domain.Modules.Base.Models;
using Domain.Modules.CategoryOfProduct.Consts;
using Domain.Modules.CategoryOfProduct.ViewModels;
using Shared.Enums;
using Web.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Web.Api.Controllers.Base;

namespace Web.Api.Controllers.CategoryOfProduct
{
    [Serializable]
    [Area(CategoryOfProductConsts.ControllerName)]
    [Route(CategoryOfProductConsts.Url)]
    public class CategoryOfProductViewController : BaseController
    {
        /// <summary>
        /// Constructor class.
        /// </summary>
        public CategoryOfProductViewController(
            IMediator mediator,
            IMapper mapper,
            IDbContext dbContext,
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.View, userAccessor);
        }

        [HttpGet]
        [UserAuthorization(OperationType = OperationEnum.View, ClaimValue = CategoryOfProductConsts.ControllerName)]
        public IActionResult Index()
        {
            logger.Info($"Index()");
            var model = new CategoryOfProductViewModel(definitionModel);
            return View(model.ViewName, model);
        }
    }
}