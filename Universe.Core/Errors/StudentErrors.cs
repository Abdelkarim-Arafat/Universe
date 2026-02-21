using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client;

namespace Universe.Core.Errors;

public record StudentErrors
{
    public static readonly Error DuplicateStudentCode = 
        new("Student.DuplicateStudentCode", "Another student with the same code is already exists", StatusCodes.Status409Conflict);

    public static readonly Error DuplicateNationalIdOrPassport =
        new("Student.DuplicateNationalIdOrPassport", "Another student with the same national id or passport is already exists", StatusCodes.Status409Conflict);
    public static readonly Error InvalidCredentials =
        new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);

    public static readonly Error DisabledUser =
        new("User.DisabledUser", "Disabled user, please contact your administrator", StatusCodes.Status401Unauthorized);

    public static readonly Error LockedUser =
        new("User.LockedUser", "Locked user, please contact your administrator", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedEmail =
        new("User.DuplicatedEmail", "Another user with the same email is already exists", StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed =
        new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidCode =
        new("User.InvalidCode", "Invalid code", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedConfirmation =
        new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);

    public static readonly Error UserNotFound =
    new("User.UserNotFound", "User is not found", StatusCodes.Status404NotFound);

    public static readonly Error InvalidRoles =
        new("Role.InvalidRoles", "Invalid roles", StatusCodes.Status400BadRequest);

    public static readonly Error InvalideRegister =
        new("User.InvalideRegister", "Invalid register data", StatusCodes.Status400BadRequest);
}