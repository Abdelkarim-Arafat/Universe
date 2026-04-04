using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class EnrollmentErrors
{
    //public static readonly Error NotFound =
    //  new("Enrollment.NotFound", "No Enrollment with specific id", StatusCodes.Status404NotFound);
    public static readonly Error DublicatedSessionWithSameType =
      new("Enrollment.DublicatedSessionWithSameType", "cannot register same course in two groups", StatusCodes.Status409Conflict);
    public static readonly Error DublicatedSessions =
     new("Enrollment.DublicatedSessions", "there is two sessions are overlabed", StatusCodes.Status409Conflict);
    public static readonly Error DublicatedGroup =
     new("Enrollment.DublicatedGroup", "same enrollement in course has 2 diff groups", StatusCodes.Status409Conflict);
}
