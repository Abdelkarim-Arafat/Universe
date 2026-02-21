using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Queries.GetCollegeGrades;

public record GetCollegeGradesQuery
(
    Guid CollegeId
) : IRequest<Result<List<GradeResponse>>>;
