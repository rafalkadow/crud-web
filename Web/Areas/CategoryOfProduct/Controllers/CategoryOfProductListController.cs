using AutoMapper;
using Application.Extensions;
using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.CategoryOfProduct.Consts;
using Domain.Modules.CategoryOfProduct.Queries;
using Domain.Modules.CategoryOfProduct.ViewModels;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Areas.Base.Filters;
using Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;

namespace Web.Areas.CategoryOfProduct.Controllers
{
    [Serializable]
    [Area(CategoryOfProductConsts.ControllerName)]
    [Route(CategoryOfProductConsts.Url)]
    public class CategoryOfProductListController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "CategoryOfProductListController" /> class.
        /// </summary>
        public CategoryOfProductListController(
            IMediator mediator,
            IMapper mapper,
            IDbContext dbContext,
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.List, userAccessor);
        }

        [HttpPost]
        [Route(BaseConsts.List)]
        [ValidateAntiForgeryToken]
        [UserAuthorization(OperationType = OperationEnum.View, ClaimValue = CategoryOfProductConsts.ControllerName)]
        public async Task<IActionResult> Index([FromForm] GetCategoryOfProductQueryAll filter)
        {
            logger.Info($"Index(filter='{filter.RenderProperties()}')");
            var model = new CategoryOfProductViewModel(definitionModel);
            this.Request.GetFilterDataAll(filter, model);
            var list = (IList<GetCategoryOfProductResultAll>?) await mediator.Send(filter);

            var definitionItemAll = new List<string>
			{
				"Name",
                "Code",
            };

			var response = new
            {
                data = list.Select(x =>
                {
                    var list = new List<string>();
                    list.StartListAdd(x, model);

                    foreach (var item in definitionItemAll)
                    {
                        switch (item)
                        {
                            case nameof(x.Name):
                                list.Add(x.Name);
                                break;

                            case nameof(x.Code):
                                list.Add(x.Code);
                                break;
                        }
                    }

                    list.EndListAdd(x);
                    return list.ToArray();
                }),
                draw = filter.Echo,
                recordsFiltered = filter.TotalRecords,
                recordsTotal = filter.TotalRecords,
            };

            return new JsonResult(response);
        }

        [HttpGet(BaseConsts.Get + "/{id}")]
        [UserAuthorization(OperationType = OperationEnum.View, ClaimValue = CategoryOfProductConsts.ControllerName)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            logger.Info($"GetById(id='{id}')");
            var query = new GetCategoryOfProductQueryById(id);
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}