using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Application.Validations;
using System.Linq.Expressions;
using Infrastructure.Shared.Models;
using Domain.Modules.Identity;
using Domain.Modules;
using Domain.Modules.Role;
using Domain.Modules.User;
using Domain.Modules.QueryStringParameters;
using Application.Repositories.Interfaces;
using Application.Repositories.Interfaces.Extensions;
using Domain.Modules.Communication.Generics;
using Domain.Modules.Communication;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly RoleValidator _validator;
        private readonly RoleParametersValidator _roleParametersValidator;

        public RoleService(IRoleRepository roleRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _validator = new RoleValidator();
            _roleParametersValidator = new RoleParametersValidator();
        }


        public async Task<ServiceResponse<PaginatedList<RoleReadDto>>> GetAllDtoPaginatedAsync(RoleParametersDto parameters)
        {
            var validationResult = _roleParametersValidator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<PaginatedList<RoleReadDto>>(validationResult)
                    .SetDetail($"Invalid query string parameters. See '{BaseResponse.ErrorKey}' for more details");
            }

            Expression<Func<RoleApp, bool>> expression =
                r =>
                    r.Name.ToLower().Contains(parameters.Name.ToLower()) &&
                    r.Users.Select(u => u.Id)
                        .Where(id => parameters.UserId.Contains(id))
                        .Count() == parameters.UserId.Count();

            var page = await _roleRepository.GetAllWherePaginatedAsync(parameters.PageNumber, parameters.PageSize, expression);
            var dto = _mapper.Map<PaginatedList<RoleApp>, PaginatedList<RoleReadDto>>(page);

            var result = new ServiceResponse<PaginatedList<RoleReadDto>>(dto);

            return result;
        }


        public async Task<ServiceResponse<RoleApp>> GetByIdAsync(Guid id)
        {
            var validationResult = new ValidationResult().ValidateId(id, "Role Id");
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<RoleApp>(validationResult)
                    .SetDetail($"Invalid role data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return new FailedServiceResponse<RoleApp>()
                    .SetTitle("Role not found")
                    .SetDetail($"Role [Id = {id}] not found.")
                    .SetStatus(StatusCodes.Status404NotFound);
            };

            var result = new ServiceResponse<RoleApp>(role);

            return result;
        }


        public async Task<ServiceResponse<RoleReadDto>> GetDtoByIdAsync(Guid id)
        {
            var roleResponse = await GetByIdAsync(id);
            if (!roleResponse.Success)
            {
                return new FailedServiceResponse<RoleReadDto>(roleResponse);
            }

            var role = roleResponse.Data;
            var roleDto = _mapper.Map<RoleReadDto>(role);

            var result = new ServiceResponse<RoleReadDto>(roleDto);

            return result;
        }


        public async Task<ServiceResponse<RoleApp>> GetByNameAsync(string name)
        {
            var validationResult = new ValidationResult().ValidateRoleName(name);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<RoleApp>(validationResult)
                    .SetDetail($"Invalid role data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var role = await _roleRepository.GetByNameAsync(name);
            if (role == null)
            {
                return new FailedServiceResponse<RoleApp>()
                    .SetTitle("Role not found.")
                    .SetDetail($"Role '{name}' not found.")
                    .SetStatus(StatusCodes.Status404NotFound);
            }

            var result = new ServiceResponse<RoleApp>(role);

            return result;
        }


        public async Task<ServiceResponse<RoleReadDto>> GetDtoByNameAsync(string name)
        {
            var roleResponse = await GetByNameAsync(name);
            if (!roleResponse.Success)
            {
                return new FailedServiceResponse<RoleReadDto>(roleResponse);
            }

            var role = roleResponse.Data;
            var roleDto = _mapper.Map<RoleReadDto>(role);

            var result = new ServiceResponse<RoleReadDto>(roleDto);

            return result;
        }


        public async Task<ServiceResponse<RoleReadDto>> CreateAsync(RoleWriteDto roleWriteDto)
        {
            var validationResult = _validator.Validate(roleWriteDto);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<RoleReadDto>(validationResult)
                    .SetDetail($"Invalid role data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var role = _mapper.Map<RoleApp>(roleWriteDto);

            var createResult = await _roleRepository.CreateAsync(role);
            if (!createResult.Succeeded)
            {
                return new FailedServiceResponse<RoleReadDto>(createResult)
                    .SetTitle("Error creating role")
                    .SetDetail($"Role not created. See '{BaseResponse.ErrorKey}' property for more details");
            }

            var appRole = await _roleRepository.GetByNameAsync(roleWriteDto.Name);
            var dto = _mapper.Map<RoleReadDto>(appRole);

            var result = new ServiceResponse<RoleReadDto>(dto);

            return result;
        }


        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var validationResult = new ValidationResult().ValidateId(id, "Role Id");
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse(validationResult)
                    .SetDetail($"Invalid role data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var roleResponse = await GetByIdAsync(id);
            if (!roleResponse.Success)
            {
                return new FailedServiceResponse(roleResponse);
            }

            var role = roleResponse.Data;

            if (role.Name == AppConstants.Roles.Admin.Name ||
                role.Name == AppConstants.Roles.Manager.Name ||
                role.Name == AppConstants.Roles.Stock.Name ||
                role.Name == AppConstants.Roles.Seller.Name)
            {
                return new FailedServiceResponse()
                    .SetTitle("Error deleting role")
                    .SetDetail($"Role '{role.Name}' cannot be deleted.")
                    .SetInstance(RoleInstance(id));
            }

            var deleteResult = await _roleRepository.DeleteAsync(role);
            if (!deleteResult.Succeeded)
            {
                return new FailedServiceResponse()
                    .SetTitle("Error deleting role")
                    .AddToExtensionsErrors(deleteResult.Errors.Select(e => e.Description))
                    .SetDetail($"Role not deleted. See '{BaseResponse.ErrorKey}' property for more details");
            }

            return new BaseResponse();
        }


        public async Task<ServiceResponse<PaginatedList<UserReadDto>>> GetAllUsersOnRolePaginatedAsync(Guid id, QueryStringParameterDto parameters)
        {
            var validationResult = new ValidationResult().ValidateId(id, "Role Id");
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<PaginatedList<UserReadDto>>(validationResult)
                    .SetDetail($"Invalid role data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var roleResponse = await GetByIdAsync(id);
            if (!roleResponse.Success)
            {
                return new FailedServiceResponse<PaginatedList<UserReadDto>>(roleResponse);
            }

            var role = roleResponse.Data;
            var usersOnRole = role.Users;

            var paginatedUsers = usersOnRole
                    .OrderBy(u => u.FirstName)
                    .ThenBy(p => p.Id)
                    .ToPaginatedList(parameters.PageNumber, parameters.PageSize);

            var usersReadDtoPaginated = _mapper.Map<PaginatedList<UserReadDto>>(paginatedUsers);
            var result = new ServiceResponse<PaginatedList<UserReadDto>>(usersReadDtoPaginated);

            return result;
        }


        private string RoleInstance(object id)
        {
            return _linkGenerator.GetPathByName("RoleController.GetRoleById", new { id });
        }
    }
}