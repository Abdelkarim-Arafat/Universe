using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class StudentAssessmentErrors
{
    public static readonly Error AssessmentWithoutDegree =
       new("StudentAssessment.AssessmentWithoutDegree", "Student assessment without degree", StatusCodes.Status409Conflict);

    public static readonly Error NotFound =
      new("StudentAssessment.NotFound", "Student assessment not found", StatusCodes.Status404NotFound);

    public static readonly Error AssessmentDegreeExceedsMaxScore =
      new("StudentAssessment.AssessmentDegreeExceedsMaxScore", "Student assessment degree exceeds max score", StatusCodes.Status409Conflict);
}
