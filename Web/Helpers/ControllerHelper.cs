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

        public static IActionResult SignInUserToApplication(this ControllerBase controller, SignInCommand command, OperationResult? result)
        {
            if (result != null && result.OperationStatus)
            {
                string? entityId = result.EntityId != null ? result.EntityId.ToString() : string.Empty;
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

        public static IActionResult SwitchResultJson(this ControllerBase controller, OperationResult? result)
        {
            if (result != null && result.OperationStatus)
            {
                return new JsonResult(SuccessResponse(result));
            }
            return new JsonResult(FailResponse(result));
        }


        public static OperationResultWeb FailResponse(OperationResult? operationResult = null)
        {
            if (operationResult != null)
            {
                var dictionaryValidation = new Dictionary<string, string>();

                if (operationResult.Errors != null)
                {
                    for (int i = 0; i < operationResult.Errors.Count(); i++)
                    {
                        var element = operationResult.Errors.ToList()[i];

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
                    ErrorMessage = operationResult.ErrorMessage,
                    Guid = operationResult.GuidRecord,
                    OperationType = EnumExtensions.GetDescription<OperationEnum>(operationResult.Operation),
                    Entity = operationResult.entity,
                    EntityId = operationResult.EntityId,
                    Message = operationResult.Message,
                };

                return responseAnother;
            }
            var response = new OperationResultWeb
            {
                Success = false,
            };

            return response;
        }

        public static OperationResultWeb SuccessResponse(OperationResult operationResult = null)
        {
            if (operationResult != null)
            {
                var responseWithGuid = new OperationResultWeb
                {
                    Success = true,
                    Guid = operationResult.GuidRecord,
                    OperationType = EnumExtensions.GetDescription<OperationEnum>(operationResult.Operation),
                    Entity = operationResult.entity,
                    EntityId = operationResult.EntityId,
                    Message = operationResult.Message,
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