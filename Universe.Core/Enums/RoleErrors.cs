using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Enums;

public class RoleErrors
{
    public static readonly Error NotFound = new Error(
        "Role.NotFound",
        "The specified role was not found.",
        StatusCodes.Status404NotFound);
}
