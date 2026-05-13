using Universe.Application.ExamTermServices.Dtos;

namespace Universe.Application.ExamTermServices.Queries.Get;

public record GetExamTermQuery
(
    [Required] Guid Id
) : IRequest<Result<ExamTermResponse>>;