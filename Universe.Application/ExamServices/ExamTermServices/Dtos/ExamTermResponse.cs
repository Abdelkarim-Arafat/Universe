using Universe.Core.Enums;

namespace Universe.Application.ExamServices.ExamTermServices.Dtos;

public record ExamTermResponse
(
    Guid Id,
    Guid SemesterId,
    Guid AcademicProgramId,
    ExamType ExamType,
    DateOnly StartDate,
    DateOnly EndDate,
    bool IsPublished
);