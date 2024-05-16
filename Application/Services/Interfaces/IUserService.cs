using Microsoft.AspNetCore.Identity;
using Domain.Modules.User;
using Domain.Modules;
using Domain.Modules.Role;
using Domain.Modules.QueryStringParameters;
using Domain.Modules.Identity;
using Domain.Modules.Communication.Generics;
using Domain.Modules.Communication;

namespace Application.Services
{
    public interface IUserService
    {        
        public Task<ServiceResponse<PaginatedList<UserReadDto>>> GetAllDtoPaginatedAsync(UserParametersDto parameters);

        public Task<ServiceResponse<UserApp>> GetByUserNameAsync(string userName);

        public Task<ServiceResponse<UserDetailedReadDto>> GetCurrentUserDtoAsync();

        public Task<ServiceResponse<UserDetailedReadDto>> GetDtoByIdAsync(Guid id);

        public Task<ServiceResponse<UserApp>> GetByIdAsync(Guid id);

        public Task<ServiceResponse<PaginatedList<RoleReadDto>>> GetAllRolesFromUserPaginatedAsync(Guid id, QueryStringParameterDto parameters);
                
        public Task<ServiceResponse<IdentityResult>> CreateAsync(UserApp user, string password);

        public Task<ServiceResponse<UserReadDto>> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto);

        public Task<BaseResponse> ChangePasswordAsync(Guid id, ChangePasswordDto changePasswordDto);

        public Task<BaseResponse> ChangeCurrentUserPasswordAsync(ChangePasswordDto changePasswordDto);

        public Task<BaseResponse> ResetPasswordAsync(Guid id, string newPassword);

        public Task<ServiceResponse<UserReadDto>> AddToRoleAsync(Guid id, Guid userId);

        public Task<ServiceResponse<UserReadDto>> RemoveFromRoleAsync(Guid id, Guid userId);

        public Task<ServiceResponse<IList<string>>> GetRolesNamesAsync(string userName);

        public Task<BaseResponse> ResetTestUsers();

        public Task<BaseResponse> DeleteAsync(Guid id);


    }
}