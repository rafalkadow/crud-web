﻿using Application.Services;
using Domain.Modules.Product;
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
    /// Operations with the list of products the store works
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/v1/products")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        /// <summary>
        /// Finds all products, filtering the result
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<ProductReadDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "string", Descriptions.XPaginationDescription)]
        [HttpGet(Name = nameof(GetAllProductsPaginated))]
        public async Task<IActionResult> GetAllProductsPaginated([FromQuery] ProductParametersDto parameters)
        {
            var response = await _productService.GetAllDtoPaginatedAsync(parameters);
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
        /// Finds an product by Id
        /// </summary>
        /// <remarks>Returns a single product</remarks>
        /// <param name="id">The product's Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Product found")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        [HttpGet("{id}", Name = nameof(GetProductById))]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var response = await _productService.GetDtoByIdAsync(id);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return Ok(result);
        }



        /// <summary>
        /// Adds a new product
        /// </summary>
        /// <remarks>Create a new product and a new product stock for it, with the initial quantity informed</remarks>
        /// <param name="product">Product object to be add to store</param>
        /// <param name="quantity">Initial quantity of product in stock</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status201Created, "Product created", typeof(ProductReadWithStockDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = nameof(CreateProduct))]
        public async Task<IActionResult> CreateProduct(ProductWriteDto product, int quantity)
        {
            var response = await _productService.CreateAsync(product, quantity);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return CreatedAtRoute(nameof(GetProductById), new { result.Id }, result);
        }


        /// <summary>
        /// Delete an existing product
        /// </summary>
        /// <param name="id">The product Id</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        [HttpDelete("{id}", Name = nameof(DeleteProduct))]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = await _productService.DeleteAsync(id);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            return Ok();
        }


        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">Product's Id</param>
        /// <param name="productUpdate">Product's updated data</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product updated", typeof(ProductWriteDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}", Name = nameof(UpdateProduct))]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductWriteDto productUpdate)
        {
            var response = await _productService.UpdateAsync(id, productUpdate);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return Ok(result);
        }
    }
}