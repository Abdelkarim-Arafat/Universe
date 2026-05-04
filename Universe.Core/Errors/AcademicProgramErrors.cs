using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public record AcademicProgramErrors
{
    public static readonly Error AlreadyExists =
    new("AcademicProgram.AlreadyExists", "Program name or code already exists", StatusCodes.Status409Conflict);

    public static readonly Error NotFound = new(
        "AcademicProgram.NotFound",
        "Program is not found",
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
