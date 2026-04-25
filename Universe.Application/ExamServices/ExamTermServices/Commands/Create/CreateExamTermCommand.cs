using Universe.Application.ExamServices.ExamTermServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.ExamServices.ExamTermServices.Commands.Create;
 
public record CreateExamTermCommand
(
    ExamType ExamType,
    DateOnly StartDate,
    DateOnly EndDate,
    [Required] Guid SemesterId,
    [Required] Guid AcademicProgramId
) : IRequest<Result<ExamTermResponse>>;