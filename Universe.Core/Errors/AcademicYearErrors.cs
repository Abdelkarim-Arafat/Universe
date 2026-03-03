using Microsoft.AspNetCore.Http;

using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class AcademicYearErrors
{
    public static readonly Error MakeConflict = new Error(
        "AcademicYear.Conflict",
        "Academic Year Data making Conflict with onother year",
        StatusCodes.Status409Conflict
    );

    public static readonly Error NotFound = new Error(
        "AcademicYear.NotFound",
        "Academic Year is not found",
        StatusCodes.Status400BadRequest
    );
}
