using Universe.Core.Enums;

namespace Universe.Application.ExamTermServices.Commands.Update;

public record UpdateExamTermCommand
(
    [Required] Guid Id,
     ExamType ExamType,
     DateOnly StartDate,
     DateOnly EndDate
) : IRequest<Result>;