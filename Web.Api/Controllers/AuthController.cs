﻿using Application.Helpers;
using Application.Services;
using Domain.Modules.Auth;
using Domain.Modules.Role;
using Domain.Modules.User;
using Infrastructure.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Web.Api.Controllers
{
    /// <summary>
    /// Registration and JWT authentication
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/auth/")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(void))]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ITestAccountService _testUserService;

        public AuthController(IAuthService authService, IUserService userService, IRoleService roleService, ITestAccountService testUserService)
        {
            _authService = authService;
            _userService = userService;
            _roleService = roleService;
            _testUserService = testUserService;
        }



        /// <summary>
        /// Registers new user
        /// </summary>
        /// <remarks>
        /// <para>Registers a new user and authenticates it, generating a valid JWT</para>
        /// - First name must have less than or equal to 50 chars
        /// - Last name must have less than or equal to 50 chars
        /// - User needs to be 18 or older. 
        /// - Passwords must have between 4 and 50 chars 
        /// </remarks>
        /// <seealso cref="AppConstants.Validations.User"/>
        /// <param name="userDto">New user's data</param>
        [HttpPost("register", Name = nameof(Register))]
        [SwaggerResponse(StatusCodes.Status201Created, "User successfully registered and authenticated", typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error registering user")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            var registerResponse = await _authService.RegisterAsync(userDto);
            if (!registerResponse.Success)
            {
                return new ErrorDetailsObjectResult(registerResponse.Error);
            }

            var authenticateResponse = await _authService.AuthenticateAsync(userDto);
            if (!authenticateResponse.Success)
            {
                return new ErrorDetailsObjectResult(authenticateResponse.Error);
            }

            var authResult = authenticateResponse.Data;

            return CreatedAtRoute(nameof(UserController.GetUserById), new { authResult.User.Id }, authResult);
        }


        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <remarks>Authenticates an user, generating a valid JWT</remarks>
        /// <param name="userLogin">User's username and password</param>
        [HttpPost("authenticate", Name = nameof(Authenticate))]
        [SwaggerResponse(StatusCodes.Status200OK, "User authenticated", typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        public async Task<IActionResult> Authenticate(UserLoginDto userLogin)
        {
            var authResponse = await _authService.AuthenticateAsync(userLogin);
            if (!authResponse.Success)
            {
                return new ErrorDetailsObjectResult(authResponse.Error);
            }

            var result = authResponse.Data;

            return Ok(result);
        }

        /// <summary>
        /// Quick registration for a new test user with roles assigned
        /// </summary>
        /// <remarks>
        /// Data is generating consuming an external API for random user data. If any error occurs, the random data is generated internally.
        /// Default password: 'test'.
        ///
        /// Example:
        /// - Username: randomusername123
        /// - Password: test
        /// </remarks>
        /// <param name="roleId">Roles assigned to the test user</param>
        [HttpPost("register/testAccount", Name = nameof(RegisterTestAcc))]
        [SwaggerResponse(StatusCodes.Status201Created, "User successfully registered and authenticated", typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error registering user")]
        public async Task<IActionResult> RegisterTestAcc([FromQuery] List<RolesEnum> roleId)
        {
            var authenticateResponse = await _testUserService.RegisterTestAcc(roleId);
            if (!authenticateResponse.Success)
            {
                return new ErrorDetailsObjectResult(authenticateResponse.Error);
            }

            var authResult = authenticateResponse.Data;

            return CreatedAtRoute(nameof(UserController.GetUserById), new { authResult.User.Id }, authResult);
        }



        /// <summary>
        /// Quick authentication with a default test acc
        /// </summary>
        /// <param name="user">Test user to authenticate</param>
        [HttpPost("authenticate/testAccount/{user}", Name = nameof(AuthenticateTestUser))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        public async Task<IActionResult> AuthenticateTestUser(UserEnum user)
        {
            var resetResult = await _userService.ResetTestUsers();
            if (!resetResult.Success)
            {
                return new ErrorDetailsObjectResult(resetResult.Error);
            }

            var testUserLogin = TestAccountUsersLoginFactory.Generate(user);

            var authResponse = await _authService.AuthenticateAsync(testUserLogin);
            if (!authResponse.Success)
            {
                return new ErrorDetailsObjectResult(authResponse.Error);
            }

            var result = authResponse.Data;

            return Ok(result);
        }
    }
}