using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class ExamErrors
{
    #region ExamTerm
    public static readonly Error ExamTermNotFound =
     new("ExamTerm.ExamTermNotFound", "The exam term is not found", StatusCodes.Status404NotFound);

    public static readonly Error OverlabbingTime =
     new("ExamTerm.OverlabbingTime", "The is another exam with overlabed time", StatusCodes.Status409Conflict);
    #endregion


    #region CourseOfferingExam
    public static readonly Error CourseOfferingExamNotFound =
    new("CourseOfferingExam.CourseOfferingExamNotFound", "The course offering exam is not found", StatusCodes.Status404NotFound);
    #endregion
}
