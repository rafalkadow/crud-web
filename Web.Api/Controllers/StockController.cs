﻿using Application.Services;
using Domain.Modules.ProductStock;
using Domain.Modules.QueryStringParameters;
using Infrastructure.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;
using Web.Api.Swagger;

namespace Web.Api.Controllers
{
    /// <summary>
    /// Product stocks operations
    /// </summary>
    [ApiController]
    [Route("api/v1/stock")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(void))]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }


        /// <summary>
        /// Finds all stocks of all products, filtering the result
        /// </summary>
        /// <remarks>The results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductStockReadDto))]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "string", Descriptions.XPaginationDescription)]
        [Authorize(Roles = "Administrator,Manager,Stock,Seller")]
        [HttpGet(Name = nameof(GetAllStocksPaginated))]
        public async Task<IActionResult> GetAllStocksPaginated([FromQuery] StockParametersDto parameters)
        {
            var response = await _stockService.GetAllDtoPaginatedAsync(parameters);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var page = response.Data;
            var metadata = page.GetMetadata();
            var result = page.Items;

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result);
        }


        /// <summary>
        /// Finds a product stock by the product Id
        /// </summary>
        /// <remarks>Returns a single product stock</remarks>
        /// <param name="productId">The product stock's product Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Product stock found", typeof(ProductStockReadDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator,Manager,Stock,Seller")]
        [HttpGet("product/{productId}", Name = nameof(GetStockByProductId))]
        public async Task<IActionResult> GetStockByProductId(Guid productId)
        {
            var response = await _stockService.GetDtoByProductIdAsync(productId);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return Ok(result);
        }


        /// <summary>
        /// Finds a product stock by Id
        /// </summary>
        /// <remarks>Returns a single product stock</remarks>
        /// <param name="id">The product stock Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Product stock found", typeof(ProductStockReadDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator,Manager,Stock,Seller")]
        [HttpGet("{id}", Name = nameof(GetStockById))]
        public async Task<IActionResult> GetStockById(Guid id)
        {
            var response = await _stockService.GetDtoByIdAsync(id);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return Ok(result);
        }

        /// <summary>
        /// Updates an existing product stock
        /// </summary>
        /// <remarks>Use it to fix stock quantity inconsistencies and to update data. </remarks>
        /// <param name="id">Product stock Id</param>
        /// <param name="stockUpdate">Product stock's updated data</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Stock updated", typeof(ProductStockReadDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id}", Name = nameof(UpdateStock))]
        public async Task<IActionResult> UpdateStock(Guid id, [FromBody] ProductStockWriteDto stockUpdate)
        {
            var response = await _stockService.UpdateAsync(id, stockUpdate);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return Ok(result);
        }


        /// <summary>
        /// Adds quantity to a product stock
        /// </summary>
        /// <remarks>Use this on daily stock movement (i.e. products being returned, purchasing products from supplier, etc)</remarks>
        /// <param name="id">Product stock Id</param>
        /// <param name="quantity">Quantity to add</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Added product quantity", typeof(ProductStockReadDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator,Manager,Stock")]
        [HttpPut("{id}/add/{quantity}", Name = nameof(AddQuantityToStock))]
        public async Task<IActionResult> AddQuantityToStock(Guid id, int quantity)
        {
            var response = await _stockService.AddProductQuantityAsync(id, quantity);
            if (!response.Success)
            {
                if (!response.Success)
                {
                    return new ErrorDetailsObjectResult(response.Error);
                }
            }

            var result = response.Data;

            return Ok(result);
        }


        /// <summary>
        /// Removes quantity to a product stock
        /// </summary>
        /// <remarks>Use this on daily stock movement (i.e. selling)</remarks>
        /// <param name="id">Product stock Id</param>
        /// <param name="quantity">Quantity to remove</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Removed product quantity", typeof(ProductStockReadDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator,Manager,Stock,Seller")]
        [HttpPut("{id}/remove/{quantity}", Name = nameof(RemoveQuantityFromStock))]
        public async Task<IActionResult> RemoveQuantityFromStock(Guid id, int quantity)
        {
            var response = await _stockService.RemoveProductQuantityAsync(id, quantity);
            if (!response.Success)
            {
                if (!response.Success)
                {
                    return new ErrorDetailsObjectResult(response.Error);
                }
            }

            var result = response.Data;

            return Ok(result);
        }
    }
}