using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class StudyLoadRuleErrors
{
    public static readonly Error OverLabedExist = 
        new("StudyLoadRule.OverLabedExist",
            "The provided study load rule overlaps with an existing rule.",
            StatusCodes.Status409Conflict);

    public static readonly Error NotAllowedHours =
        new("StudyLoadRule.NotAllowedHours",
            "The provided study load rule has hours that are not allowed.",
            StatusCodes.Status409Conflict);

    public static readonly Error NotFound =
        new("StudyLoadRule.NotFound",
            "The specified study load rule was not found.",
            StatusCodes.Status404NotFound);

}
