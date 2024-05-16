using Microsoft.AspNetCore.Mvc;

namespace Domain.Modules.Exceptions
{
    public class InfrastructureException : ExceptionWithProblemDetails
    {
        public InfrastructureException()
        {
        }

        public InfrastructureException(ProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}