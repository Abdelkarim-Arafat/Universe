using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class AcademicEventErrors
{
    public static readonly Error OverLabedDateTime = new Error("AcademicEvent.OverLabedDateTime",
        "The event dates overlap with an existing event.",
        StatusCodes.Status409Conflict);

    public static readonly Error NotFound = new Error("AcademicEvent.NotFound",
        "The specified academic event was not found.",
        StatusCodes.Status404NotFound);
}