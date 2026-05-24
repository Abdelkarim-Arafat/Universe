using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Enums;

public class RoleErrors
{
    public static readonly Error NotFound = new Error(
        "Role.NotFound",
        "The specified role was not found.",
        StatusCodes.Status404NotFound);
}
