using Domain.Modules.CategoryOfProduct.Commands;
using Domain.Modules.CategoryOfProduct.Consts;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.GeneralExtensions;
using MediatR;
using Web.Api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using Infrastructure.Shared.Models;
using Swashbuckle.AspNetCore.Annotations;
using Shared.Models;
using Domain.Modules.CategoryOfProduct.Queries;
using Domain.Modules.Communication.Generics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Domain.Modules.Product;
using Domain.Modules.QueryStringParameters;
using Swashbuckle.AspNetCore.Filters;
using Web.Api.Swagger;
using NLog.Filters;

namespace Web.Api.Controllers.CategoryOfProduct
{
    [Serializable]
    [Authorize]
    [ApiController]
    [Route("api/v1/" + CategoryOfProductConsts.Url)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class CategoryOfProductController : BaseController
    {
        /// <summary>
        /// Constructor class.
        /// </summary>
        public CategoryOfProductController(IMediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Finds all category of products, filtering the result
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GetCategoryOfProductResultAll>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "string", Descriptions.XPaginationDescription)]
        [HttpGet(Name = nameof(GetAllPaginated))]
        public async Task<IActionResult> GetAllPaginated([FromQuery] GetCategoryOfProductQueryAll parameters)
        {
            logger.Info($"Index(parameters='{parameters.RenderProperties()}')");
            var response = await mediator.Send(parameters) as ServiceResponse<IList<GetCategoryOfProductResultAll>>;
            if (response == null || !response.Success)
            {
                return new ErrorDetailsObjectResult(response == null ? new ServiceResponse<OperationResult>(new OperationResult(false)) : response.Error);
            }

            var page = response.Data;
            //var metadata = page.GetMetadata();
            //var result = page.Items;

            //Response.Headers.Add("X-Pagination", metadata);

            return Ok(response.Data);
        }

        /// <summary>
        /// Finds an category of product by Id
        /// </summary>
        /// <remarks>Returns a single category of product</remarks>
        /// <param name="Id">The category of product's Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Category of product found")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category of product not found")]
        [HttpGet("{id}", Name = nameof(GetDataById))]
        public async Task<IActionResult> GetDataById(Guid Id)
        {
            logger.Info($"Action(Id='{Id}')");
            var response = await mediator.Send(new GetCategoryOfProductQueryById(Id));
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }
            var result = response.Data;
            return Ok(result);
        }

        /// <summary>
        /// Adds a new category of product
        /// </summary>
        /// <remarks>Create a new category of product</remarks>
        /// <param name="command">Category of product object</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status201Created, "Category of product created", typeof(OperationResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = nameof(Create))]
        public async Task<IActionResult> Create(CreateCategoryOfProductCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Errors);
            }
            return CreatedAtRoute(nameof(Create), new { response.Data.Id }, response);
        }

        /// <summary>
        /// Update category of product
        /// </summary>
        /// <remarks>Update category of product</remarks>
        /// <param name="command">Category of product object</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status201Created, "Category of product updated", typeof(OperationResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPut("{Id}", Name = nameof(Update))]
        public async Task<IActionResult> Update(Guid Id, UpdateCategoryOfProductCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Errors);
            }
            var result = response.Data;
            return Ok(result);
        }

        /// <summary>
        /// Delete an existing category of product
        /// </summary>
        /// <param name="command">The category of product Id</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category of product deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category of product not found")]
        [HttpDelete("{command}", Name = nameof(Delete))]
        public async Task<IActionResult> Delete(DeleteCategoryOfProductCommand command)
        {
            logger.Info($"Action(command='{command.RenderProperties()}')");
            var response = await mediator.Send(command) as ServiceResponse<OperationResult>;
            if (response == null || !response.Success)
            {
                return new ErrorDetailsObjectResult(response == null ? new ServiceResponse<OperationResult>(new OperationResult(false)) : response.Error);
            }
            return Ok();
        }

    }
}