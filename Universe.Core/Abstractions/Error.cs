using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Abstractions;

public class Error
{
    public string Code { get; }
    public string Description { get; }
    public int StatusCode { get; }

    public IReadOnlyDictionary<string, string[]>? Failures { get; }

    public static readonly Error None =
        new(string.Empty, string.Empty, StatusCodes.Status200OK);

    public Error(
        string code,
        string message,
        int statusCode,
        IReadOnlyDictionary<string, string[]>? failures = null)
    {
        Code = code;
        Description = message;
        StatusCode = statusCode;
        Failures = failures;
    }
}
