using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public record CollegeErrors
{
    public static readonly Error NotFound =
        new("College.NotFound", "No College with specific id", StatusCodes.Status404NotFound);
}