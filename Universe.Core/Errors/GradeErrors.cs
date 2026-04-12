using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public record GradeErrors
{
    public static readonly Error InvalidScores =
        new("Grade.InvalidScores", "Grades scores or points can't be overlapped!", StatusCodes.Status409Conflict);
    public static readonly Error NotFound =
        new("Grade.NotFound", "No Grade with specific id", StatusCodes.Status404NotFound);
}