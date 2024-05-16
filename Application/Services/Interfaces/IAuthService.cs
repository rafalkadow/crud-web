using Domain.Modules.Auth;
using Domain.Modules.Communication.Generics;
using Domain.Modules.User;

namespace Application.Services
{
    public interface IAuthService
    {
        public Task<ServiceResponse<AuthResponse>> RegisterAsync(UserRegisterDto userDto);

        public Task<ServiceResponse<AuthResponse>> AuthenticateAsync(UserLoginDto userLogin);

        public Task<ServiceResponse<AuthResponse>> AuthenticateAsync(UserRegisterDto userRegisterDto);
    }
}