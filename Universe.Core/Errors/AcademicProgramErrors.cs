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

    public static readonly Error ScheduleAlreadyExist = new Error(
        "AcademicProgramErrors.ScheduleAlreadyExist",
        "This Program Already having Scheudle",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error ScheduleNotFound = new Error(
        "AcademicProgramErrors.ScheduleNotFound",
        "The Shcedule of program is not found",
        StatusCodes.Status404NotFound
    );
}
