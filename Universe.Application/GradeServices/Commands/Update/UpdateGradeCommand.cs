namespace Universe.Application.GradeServices.Commands.Update;

public record UpdateGradeCommand
(
    Guid Id,
    string Name,
    string Code,
    int MinScore,
    int MaxScore,
    decimal MinGradePoint,
    decimal MaxGradePoint
) : IRequest<Result<GradeResponse>>;
