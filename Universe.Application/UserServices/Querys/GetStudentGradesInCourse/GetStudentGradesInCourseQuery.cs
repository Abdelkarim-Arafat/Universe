using Universe.Core.Contracts.StudentAssessments;

namespace Universe.Application.UserServices.Querys.GetStudentGradesInCourse;

public record GetStudentGradesInCourseQuery
(Guid StudentId, Guid CourseOfferingId) : IRequest<Result<List<StudentAssessmentInCourseResponse>>>;
