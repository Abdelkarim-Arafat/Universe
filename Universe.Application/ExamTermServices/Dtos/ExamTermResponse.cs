using Universe.Core.Enums;

namespace Universe.Application.ExamTermServices.Dtos;

public record ExamTermResponse
(
    Guid Id,
    ExamType ExamType,
    DateOnly StartDate,
    DateOnly EndDate,
    bool IsPublished
);