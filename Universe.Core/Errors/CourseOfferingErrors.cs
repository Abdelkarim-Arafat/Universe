using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class CourseOfferingErrors
{
    public static readonly Error AlreadyExist = new Error(
        "CourseOffering.AlreadyExist",
        "This Course Already Exist",
        StatusCodes.Status409Conflict
    );

    public static readonly Error NotFound = new Error(
        "CourseOffering.NotFound",
        "This Course Not Found",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error NotValidGroupNumber = new Error(
        "CourseOffering.NotValidGroupNumber",
        "This Group Number is not valid",
        StatusCodes.Status400BadRequest
    );
}


