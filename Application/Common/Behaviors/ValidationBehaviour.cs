﻿using Domain.Modules.Communication;
using Domain.Modules.Communication.Generics;
using FluentValidation;
using MediatR;
using Shared.Models;

namespace Application.Common.Behaviors
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Any())
                {
                    //var operationResult = new OperationResult(false);
                    //operationResult.FailureAdd(failures);
                    var response = new ServiceResponse<OperationResult>();
                    response.HandleErrorParameter(validationResults.First());
                    //response.FailureAdd(failures);
                    return (TResponse)(dynamic)response;
                }
            }
            return await next();
        }
    }
}
