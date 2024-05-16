using Application.Validations;
using AutoMapper;
using Domain.Modules.Auth;
using Domain.Modules.Communication;
using Domain.Modules.Communication.Generics;
using Domain.Modules.Identity;
using Domain.Modules.User;
using Infrastructure.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UserApp> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        private readonly UserLoginValidator _userLoginValidator;
        private readonly UserRegisterValidator _userRegisterValidator;

        public AuthService(SignInManager<UserApp> signInManager, IOptions<AppSettings> appSettings, IUserService userService, IMapper mapper)
        {
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _userService = userService;
            _mapper = mapper;
            _userLoginValidator = new UserLoginValidator();
            _userRegisterValidator = new UserRegisterValidator();
        }



        public async Task<ServiceResponse<AuthResponse>> RegisterAsync(UserRegisterDto userDto)
        {
            var validationResult = _userRegisterValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<AuthResponse>(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid user data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var user = _mapper.Map<UserApp>(userDto);

            var createResponse = await _userService.CreateAsync(user, userDto.Password);
            if (!createResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(createResponse)
                    .SetTitle("Error on user registration")
                    .SetDetail($"See '{BaseResponse.ErrorKey}' property for more details");
            }

            var appUserResponse = await _userService.GetByUserNameAsync(user.UserName);
            if (!appUserResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(appUserResponse);
            }

            var appUser = appUserResponse.Data;


            var userLogin = _mapper.Map<UserLoginDto>(appUser);
            userLogin.Password = userDto.Password;

            var loginResponse = await AuthenticateAsync(userLogin);
            if (!loginResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(loginResponse)
                    .SetDetail($"User succesfully registered, but an error occurred during authentication. See '{BaseResponse.ErrorKey}' for more details");
            }

            var loginData = loginResponse.Data;

            var dto = _mapper.Map<UserAuthDto>(appUser);

            var resultData = new AuthResponse(dto, loginData.Token);
            var result = new ServiceResponse<AuthResponse>(resultData);

            return result;
        }


        public async Task<ServiceResponse<AuthResponse>> AuthenticateAsync(UserLoginDto userDto)
        {
            var validationResult = _userLoginValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<AuthResponse>(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid user data. See '{BaseResponse.ErrorKey}' for more details");
            }

            var userResponse = await _userService.GetByUserNameAsync(userDto.UserName);
            if (!userResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(userResponse.Error);
            }

            var user = userResponse.Data;

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);
            if (!signInResult.Succeeded)
            {
                if (signInResult.IsLockedOut || signInResult.IsNotAllowed)
                {
                    return new FailedServiceResponse<AuthResponse>(signInResult, user)
                        .SetTitle("Error authenticating user")
                        .SetDetail($"See '{BaseResponse.ErrorKey}' for more details");
                }

                return new FailedServiceResponse<AuthResponse>().SetTitle("Incorrect user/password");
            }

            var appUserResponse = await _userService.GetByUserNameAsync(userDto.UserName);
            if (!appUserResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(appUserResponse.Error);
            }

            var appUser = appUserResponse.Data;

            var jwtResponse = await GenerateJWTAsync(appUser);
            if (!jwtResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(jwtResponse);
            }

            var token = jwtResponse.Data;

            var userToReturn = _mapper.Map<UserAuthDto>(appUser);

            var resultData = new AuthResponse(userToReturn, token);
            var result = new ServiceResponse<AuthResponse>(resultData);

            return result;
        }


        public async Task<ServiceResponse<AuthResponse>> AuthenticateAsync(UserRegisterDto userRegisterDto)
        {
            var userLoginDto = _mapper.Map<UserLoginDto>(userRegisterDto);
            var response = await AuthenticateAsync(userLoginDto);
            if (!response.Success)
            {
                return new FailedServiceResponse<AuthResponse>(response);
            }

            return response;
        }


        private async Task<ServiceResponse<string>> GenerateJWTAsync(UserApp user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var rolesResponse = await _userService.GetRolesNamesAsync(user.UserName);
            if (!rolesResponse.Success)
            {
                return new FailedServiceResponse<string>(rolesResponse);
            }

            var roles = rolesResponse.Data;

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var encodedPrivateKey = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var key = new SymmetricSecurityKey(encodedPrivateKey);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.ExpiresMinutes),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            var resultData = tokenHandler.WriteToken(token);
            var result = new ServiceResponse<string>(resultData);

            return result;
        }
    }
}