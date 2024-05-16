using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Validation;

namespace Domain.Modules.Communication
{
    public class BaseResponse
    {
        public bool Success { get; protected set; }
        public ProblemDetails Error { get; protected set; } = new ProblemDetails { Status = StatusCodes.Status400BadRequest };
        public const string ErrorKey = "errors";
        public IEnumerable<ErrorMessage> Errors { get; private set; }
        public BaseResponse()
        {
            Success = true;
        }

        public void FailureAdd(IList<ValidationFailure> validationFailures)
        {
            Success = false;
            Errors = validationFailures.Select(v => new ErrorMessage()
            {
                PropertyName = v.PropertyName,
                Message = v.ErrorMessage
            });
        }

        public void HandleErrorParameter(ValidationResult error)
        {
            if (!error.IsValid)
            {
                SetTitle("Validation error");
                AddToExtensionsErrors(error.Errors.Select(e => e.ErrorMessage));
                SetDetail($"Invalid data. See '{ErrorKey}' for more details");
                SetStatus(400);
                Success = false;
                Errors = error.Errors.Select(v => new ErrorMessage()
                {
                    PropertyName = v.PropertyName,
                    Message = v.ErrorMessage
                });
            }
        }

        public void SetTitle(string title)
        {
            Error.Title = title;
            Success = false;
        }

        public void SetDetail(string detail)
        {
            Error.Detail = detail;
            Success = false;
        }


        public void AddToExtensionsErrors(object errors)
        {
            Success = false;

            if (errors != null)
            {
                Error.Extensions[ErrorKey] = errors;
            }
        }


        public void SetExtensions(string key, object extension)
        {
            if (extension != null)
            {
                Error.Extensions.Add(key, extension);
            }

            Success = false;
        }


        public void SetInstance(string instance)
        {
            Error.Instance = instance;
            Success = false;
        }


        public void SetStatus(int statusCode)
        {
            Error.Status = statusCode;
            Success = false;
        }

        public IActionResult GenerateErrorActionResult()
        {
            switch (Error.Status)
            {
                case 404:
                    {
                        return new NotFoundObjectResult(Error);
                    }
                case 400:
                    {
                        return new BadRequestObjectResult(Error);
                    }
                default:
                    {
                        var action = new ObjectResult(Error)
                        {
                            StatusCode = 500
                        };

                        return action;
                    }
            }
        }
    }
}