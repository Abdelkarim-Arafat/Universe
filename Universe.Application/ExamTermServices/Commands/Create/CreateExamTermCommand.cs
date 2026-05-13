using Universe.Core.Enums;

namespace Universe.Application.ExamTermServices.Commands.Create;
 
public record CreateExamTermCommand
(
    ExamType ExamType,
    DateOnly StartDate,
    DateOnly EndDate,
    [Required] Guid SemesterId,
    [Required] Guid AcademicProgramId
) : IRequest<Result<ExamTermResponse>>;