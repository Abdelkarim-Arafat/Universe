using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class StudyLoadByLevelErrors
{
    public static readonly Error AlreadyExist =
        new("StudyLoadByLevel.AlreadyExist",
            "The provided study load  already exist.",
            StatusCodes.Status400BadRequest);

    public static readonly Error NotFound =
        new("StudyLoadByLevel.NotFound",
            "The specified study load rule was not found.",
            StatusCodes.Status404NotFound);
}
