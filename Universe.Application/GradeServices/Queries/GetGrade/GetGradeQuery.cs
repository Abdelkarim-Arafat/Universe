using Universe.Application.GradeServices.GradeDtos;
namespace Universe.Application.GradeServices.Queries.GetGrade;

public record GetGradeQuery
(
    Guid Id
) : IRequest<Result<GradeResponse>>;
