using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Commands.UpdateGrade;

public record UpdateGradeCommand
(
    Guid Id,
    string Name,
    string Code,
    int MinScore,
    int MaxScore
) : IRequest<Result<GradeResponse>>;
