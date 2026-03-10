using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class SessionErrors
{
    public static readonly Error StartOrEndTimeAreNotValid = new Error(
        "CourseOffering.StartOrEndTimeisNotValid",
        "Start or End Time is not valid",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error InstructoreIsBusy = new Error(
        "SessionErrors.InstructoreIsBusy",
        "This Instructore is busy in this time",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error RoomIsBusy = new Error(
        "SessionErrors.RoomIsBusy",
        "This Room is busy in this time",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error AlreadyUsedTime = new Error(
        "SessionErrors.AlreadyUsedTime",
        "This Time is already used",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error NotFound = new Error(
        "SessionErrors.NotFound",
        "This Session is not found",
        StatusCodes.Status404NotFound
    );
}
