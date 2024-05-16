using Domain.Modules;
using Domain.Modules.Communication;
using Domain.Modules.Communication.Generics;
using Domain.Modules.Identity;
using Domain.Modules.QueryStringParameters;
using Domain.Modules.Role;
using Domain.Modules.User;

namespace Application.Services
{
    public interface IRoleService
    {
        public Task<ServiceResponse<PaginatedList<RoleReadDto>>> GetAllDtoPaginatedAsync(RoleParametersDto parameters);

        public Task<ServiceResponse<RoleApp>> GetByIdAsync(Guid id);

        public Task<ServiceResponse<RoleReadDto>> GetDtoByIdAsync(Guid id);

        public Task<ServiceResponse<RoleApp>> GetByNameAsync(string name);

        public Task<ServiceResponse<RoleReadDto>> GetDtoByNameAsync(string name);

        public Task<ServiceResponse<PaginatedList<UserReadDto>>> GetAllUsersOnRolePaginatedAsync(Guid id, QueryStringParameterDto parameters);

        public Task<ServiceResponse<RoleReadDto>> CreateAsync(RoleWriteDto role);

        public Task<BaseResponse> DeleteAsync(Guid id);
    }
}