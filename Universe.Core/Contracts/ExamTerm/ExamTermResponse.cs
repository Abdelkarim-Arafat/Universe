using Universe.Core.Enums;
namespace Universe.Core.Contracts.ExamTerm;

public record ExamTermResponse
(
    Guid Id,
    ExamType ExamType,
    DateOnly StartDate,
    DateOnly EndDate,
    bool IsPublished
);