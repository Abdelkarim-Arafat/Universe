
namespace Universe.Application.GradeServices.Commands.Create;

public record CreateGradeCommand
(
    Guid AcademicProgramId,
    string Name,
    string Code,
    int MinScore,
    int MaxScore,
    decimal MinGradePoint,
    decimal MaxGradePoint
) : IRequest<Result<GradeResponse>>;
