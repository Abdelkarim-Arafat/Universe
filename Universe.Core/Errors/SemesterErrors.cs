using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class SemesterErrors
{
    public static readonly Error OverLabedDateTime = 
        new("Semester.OverLabedDateTime",
            "The provided semesters are overlaping.",
            StatusCodes.Status409Conflict);

    public static readonly Error NotFound = new Error(
        "Semester.NotFound",
        "Semester is not found",
        StatusCodes.Status400BadRequest
    );
}
