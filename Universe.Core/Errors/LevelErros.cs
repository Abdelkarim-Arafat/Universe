using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public record LevelErrors
{
    public static readonly Error InvalidHours =
        new("Level.InvalidHours", "Levels Can't be overlabed!", StatusCodes.Status409Conflict);
    public static readonly Error NotFound =
        new("Level.NotFound", "No level with specific id", StatusCodes.Status404NotFound);
}