namespace Universe.Application.GradeServices.Dtos;

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
