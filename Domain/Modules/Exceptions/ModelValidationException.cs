using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Modules.Exceptions
{
    public class ModelValidationException : ExceptionWithProblemDetails
    {
        public ModelValidationException()
        {
        }

        public ModelValidationException(ModelStateDictionary modelState) : base(new ValidationProblemDetails(modelState))
        {
        }
    }
}