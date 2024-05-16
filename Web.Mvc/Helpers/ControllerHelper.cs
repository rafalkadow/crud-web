using Domain.Modules.Base.Consts;
using Domain.Modules.SignIn.Commands;
using Domain.Modules.SignIn.ViewModels;
using Shared.Enums;
using Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Shared.Extensions.EnumExtensions;
using Domain.Modules.Communication.Generics;

namespace Web.Helpers
{
    public static class ControllerHelper
    {
        public static IActionResult SwitchResult(this ControllerBase controller, object result)
        {
            if (result == null)
            {
                return controller.NotFound();
            }

            if (result is OperationResult operationResult)
            {
                return SwitchOperationResult(controller, operationResult);
            }

            return controller.Ok(result);
        }

        public static IActionResult SignInUserToApplication(this ControllerBase controller, SignInCommand command, ServiceResponse<OperationResult>? result)
        {
            if (result != null && result.Success)
            {
                string? entityId = result.Data.EntityId != null ? result.Data.EntityId.ToString() : string.Empty;
                List <Claim> claims = new()
                {
                    new Claim(ClaimTypes.Name, command.SignInEmail),
                    new Claim(ClaimTypes.NameIdentifier, entityId.ToString()),
                };
                //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity
                var principal = new ClaimsPrincipal(identity);
                //SignInAsync is a Extension method for Sign in a principal for the specified scheme.
                controller.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties()
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    });
                controller.HttpContext.Session.SetString(nameof(SignInViewModel.SignInEmail), command.SignInEmail);
            }
            return SwitchResultJson(controller, result);
        }

        public static string SignOutUserToApplication(this ControllerBase controller)
        {
            controller.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            string email = controller.HttpContext.Session.GetString(nameof(SignInViewModel.SignInEmail));
            controller.HttpContext.Session.Remove(nameof(SignInViewModel.SignInEmail));
            return email;
        }

        public static IActionResult SwitchResultJson(this ControllerBase controller, ServiceResponse<OperationResult>? result)
        {
            if (result != null && result.Success)
            {
                return new JsonResult(SuccessResponse(result));
            }
            return new JsonResult(FailResponse(result));
        }


        public static OperationResultWeb FailResponse(ServiceResponse<OperationResult>? result = null)
        {
            if (result != null)
            {
                var dictionaryValidation = new Dictionary<string, string>();

                if (result.Errors != null)
                {
                    for (int i = 0; i < result.Errors.Count(); i++)
                    {
                        var element = result.Errors.ToList()[i];

                        if (!dictionaryValidation.ContainsKey(element.PropertyName))
                        {
                            if (element.PropertyName == BaseValidationConsts.FluentValidationErrorCustom)
                            {
                                dictionaryValidation.Add(element.PropertyName, $"{element.Message}");
                            }
                            else
                            {
                                dictionaryValidation.Add(element.PropertyName, $"<span class='help-block'>{element.Message}</span>");
                            }
                        }
                    }
                }

                var responseAnother = new OperationResultWeb
                {
                    Success = false,
                    Messages = dictionaryValidation,
                    ErrorMessage = result.Data.ErrorMessage,
                    Guid = result.Data.GuidRecord,
                    OperationType = result.Data.Operation.GetDescription(),
                    Entity = result.Data.entity,
                    EntityId = result.Data.EntityId,
                    Message = result.Data.Message,
                };

                return responseAnother;
            }

            var response = new OperationResultWeb
            {
                Success = false,
            };

            return response;
        }

        public static OperationResultWeb SuccessResponse(ServiceResponse<OperationResult> result)
        {
            if (result != null)
            {
                var responseWithGuid = new OperationResultWeb
                {
                    Success = true,
                    Guid = result.Data.GuidRecord,
                    OperationType = result.Data.Operation.GetDescription(),
                    Entity = result.Data.entity,
                    EntityId = result.Data.EntityId,
                    Message = result.Data.Message,
                };
                return responseWithGuid;
            }

            var response = new OperationResultWeb
            {
                Success = true,
            };
            return response;
        }

        private static IActionResult SwitchOperationResult(ControllerBase controller, OperationResult operationResult)
        {
            if (!string.IsNullOrWhiteSpace(operationResult.ErrorMessage))
            {
                return controller.BadRequest(operationResult.ErrorMessage);
            }

            if (operationResult.EntityId.HasValue)
            {
                return controller.Ok(operationResult.EntityId);
            }

            return controller.NoContent();
        }
    }
}