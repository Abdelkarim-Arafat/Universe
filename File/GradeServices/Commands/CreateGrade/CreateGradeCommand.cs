using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Commands.CreateGrade;

public record CreateGradeCommand
(
    Guid CollegeId,
    string Name,
    string Code,
    int MinScore,
    int MaxScore
) : IRequest<Result<GradeResponse>>;
