using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public static class UserErrors
{
    public static readonly Error UserNotFound =
    new("User.UserNotFound", "User is not found", StatusCodes.Status404NotFound);
}
