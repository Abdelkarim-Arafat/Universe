
namespace Universe.Application.ExamServices.ExamTermServices.Queries.Get;

public record GetExamTermQuery
(
    [Required] Guid Id
) : IRequest<Result<ExamTermResponse>>;