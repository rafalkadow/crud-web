using Domain.Modules.Role;
using Domain.Modules.Auth;
using Domain.Modules.User;
using Domain.Modules.Communication.Generics;

namespace Application.Services
{
    public interface ITestAccountService
    {
        public Task<ServiceResponse<UserRegisterDto>> GetRandomUser();

        public Task<ServiceResponse<AuthResponse>> RegisterTestAcc(List<RolesEnum> roleId);
    }
}