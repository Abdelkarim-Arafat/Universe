namespace Universe.Application.GradeServices.GradeDtos;

public record GradeResponse
(
    Guid Id,
    string Name,
    string Code,
    int MinScore,
    int MaxScore,
    decimal MinGradePoint,
    decimal MaxGradePoint
);
