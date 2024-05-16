using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Shared.Models
{
    public class ErrorDetailsObjectResult : ObjectResult
    {
        public ErrorDetailsObjectResult(object value) : base(value)
        {
            if (value is ProblemDetails asProblemDetails)
            {
                StatusCode = asProblemDetails.Status ?? StatusCodes.Status500InternalServerError;
            }
        }

        public ErrorDetailsObjectResult(object value, int? statusCode) : this(value)
        {
            if (statusCode.HasValue)
            {
                StatusCode = statusCode;
            }
        }
    }
}