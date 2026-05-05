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
    public static readonly Error ExamTermWithSameType =
     new("ExamTerm.ExamTermWithSameType", "The is another exam with the same type ", StatusCodes.Status409Conflict);
    #endregion
    #region ExamCommittee
    public static readonly Error OverlappingTimeInCommittees =
     new("ExamCommittee.OverlabbingTimeInCommittees", "There is another course exam with overlabed time in the same committee !",
         StatusCodes.Status409Conflict);

    public static readonly Error ExamCommitteeNotFound =
    new("ExamCommittee.ExamCommitteeNotFound", "exam committee is not found", StatusCodes.Status404NotFound);

    public static readonly Error TotalCapacitiesIsNotEnough =
    new("ExamCommittee.TotalCapacitiesIsNotEnough", "total capacities is less than number of registered students !", StatusCodes.Status409Conflict);

    public static readonly Error SameCommitteeNumber =
    new("ExamCommittee.SameCommitteeNumber", "there is another committee with the same number!", StatusCodes.Status409Conflict);
    #endregion

    #region CourseOfferingExam
    public static readonly Error CourseOfferingExamNotFound =
    new("CourseOfferingExam.CourseOfferingExamNotFound", "The course offering exam is not found", StatusCodes.Status404NotFound);

    public static readonly Error CourseOfferingExamIsExist =
    new("CourseOfferingExam.CourseOfferingExamIsExist", "this course is already has exam", StatusCodes.Status409Conflict);


    #endregion
}
