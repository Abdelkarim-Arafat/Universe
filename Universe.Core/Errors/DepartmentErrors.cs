using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public record AcademicProgramErrors
{
    public static readonly Error AcademicProgramAlreadyExists =
    new("AcademicProgram.AlreadyExists", "AcademicProgram name or code already exists", StatusCodes.Status409Conflict);

    public static readonly Error AcademicProgramNotFound = new(
        "AcademicProgram.NotFound",
        "AcademicProgram is not found",
        StatusCodes.Status404NotFound);
}
