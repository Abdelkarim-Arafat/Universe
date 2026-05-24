using Universe.Core.Contracts.StudentAssessments;

namespace Universe.Application.StudentServices.Queries.GetStudentGradesInCourse;

public record GetStudentGradesInCourseQuery(
    Guid StudentId, Guid CourseOfferingId
) : IRequest<Result<List<StudentAssessmentInCourseResponse>>>;
