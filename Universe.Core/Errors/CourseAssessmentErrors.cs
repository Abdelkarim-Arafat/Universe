using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class CourseAssessmentErrors
{
    public static readonly Error NotFound =
       new("CourseAssessment.NotFound", "Course assessment not found", StatusCodes.Status404NotFound);
}
