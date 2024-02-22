﻿using AutoMapper;
using Application.Extensions;
using Domain.Interfaces;
using Domain.Modules.Base.Consts;
using Domain.Modules.Base.Models;
using Domain.Modules.Product.Consts;
using Domain.Modules.Product.Queries;
using Domain.Modules.Product.ViewModels;
using Shared.Enums;
using Web.Areas.Base.Controllers;
using Web.Areas.Base.Filters;
using Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;

namespace Web.Areas.Product.Controllers
{
    [Serializable]
    [Area(ProductConsts.ControllerName)]
    [Route(ProductConsts.Url + "/" + BaseConsts.List)]
    public class ProductListController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "ProductListController" /> class.
        /// </summary>
        public ProductListController(
            IMediator mediator,
            IMapper mapper, 
            IDbContext dbContext, 
            IUserAccessor userAccessor)
            : base(mediator, mapper, dbContext, userAccessor)
        {
            this.definitionModel = new DefinitionModel(OperationEnum.List, userAccessor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(OperationType = OperationEnum.View, ClaimValue = ProductConsts.ControllerName)]
        public async Task<IActionResult> Index([FromForm] GetProductQueryAll filter)
        {
            logger.Info($"Index(filter='{filter.RenderProperties()}')");
            var model = new ProductViewModel(definitionModel);
            this.Request.GetFilterDataAll(filter, model);
            var list = (IList<GetProductResultAll>?) await mediator.Send(filter);

            var definitionItemAll = new List<string>
			{
				"Name",
                "Code",
                "CategoryOfProduct"
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

                            case nameof(x.CategoryOfProduct):
                                list.Add(x.CategoryOfProduct.Name);
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
    }
}