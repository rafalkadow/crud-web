﻿using Application.Services;
using Domain.Modules.QueryStringParameters;
using Domain.Modules.Role;
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
    /// Operations about roles
    /// </summary>
    [ApiController]
    [Route("api/v1/roles")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(void))]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        /// <summary>
        /// Finds all roles, filtering the result
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<RoleReadDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "string", Descriptions.XPaginationDescription)]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet(Name = nameof(GetAllRolesPaginated))]
        public async Task<IActionResult> GetAllRolesPaginated([FromQuery] RoleParametersDto parameters)
        {
            var response = await _roleService.GetAllDtoPaginatedAsync(parameters);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var page = response.Data;
            var result = page.Items;
            var metadata = page.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result);
        }


        /// <summary>
        /// Finds a role by Id
        /// </summary>
        /// <remarks>Returns a single role</remarks>
        /// <param name="id">The role's Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Role found", typeof(RoleReadDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("{id:int}", Name = nameof(GetRoleById))]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var response = await _roleService.GetDtoByIdAsync(id);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return Ok(result);
        }


        /// <summary>
        /// Finds a role by name
        /// </summary>
        /// <remarks>Name must be an exact match, but it is not case-sensitive</remarks>
        /// <param name="roleName">The role's name</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Role found", typeof(RoleReadDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("{roleName}", Name = nameof(GetRoleByName))]
        public async Task<IActionResult> GetRoleByName(string roleName)
        {
            var response = await _roleService.GetDtoByNameAsync(roleName);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return Ok(result);
        }

        /// <summary>
        /// Finds all users on a role, filtering the result
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        /// <param name="id">Role's Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<RoleReadDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "string", Descriptions.XPaginationDescription)]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("{id}/users", Name = nameof(GetUsersOnRole))]
        public async Task<IActionResult> GetUsersOnRole(Guid id, [FromQuery] QueryStringParameterDto parameters)
        {
            var response = await _roleService.GetAllUsersOnRolePaginatedAsync(id, parameters);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var page = response.Data;
            var result = page.Items;
            var metadata = page.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result);
        }


        /// <summary>
        /// Create a new role
        /// </summary>
        /// <remarks>Role names are unique</remarks>
        /// <param name="roleDto">Role object to be created</param>
        [SwaggerResponse(StatusCodes.Status201Created, "Role created", typeof(RoleReadDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost(Name = nameof(CreateRole))]
        public async Task<IActionResult> CreateRole(RoleWriteDto roleDto)
        {
            var response = await _roleService.CreateAsync(roleDto);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return CreatedAtRoute(nameof(GetRoleById), new { result.Id }, result);
        }


        /// <summary>
        /// Delete an existing role
        /// </summary>
        /// <param name="id">The role Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Role deleted")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}", Name = nameof(DeleteRole))]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var response = await _roleService.DeleteAsync(id);
            if (!response.Success)
            {
                return new ErrorDetailsObjectResult(response.Error);
            }

            return NoContent();
        }
    }
}