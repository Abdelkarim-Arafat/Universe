
namespace Universe.Application.ExamTermServices.Queries.GetProgramExams;

public record GetProgramExamsQuery
(
    [Required]Guid AcademicProgramId,
    [Required]Guid SemesterId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<ExamTermResponse>>>;
