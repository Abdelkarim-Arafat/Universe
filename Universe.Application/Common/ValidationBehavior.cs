using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.Common;

public class ValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (!failures.Any())
            return await next();

        var errors = failures
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );

        var error = new Error(
            code: "Validation.Failed",
            message: "One or more validation errors occurred.",
            statusCode: StatusCodes.Status400BadRequest,
            failures: errors
        );

        return (TResponse)Result.Failure(error);
    }
}
