namespace Universe.Application.ExamCommitteeServices.Queries.Get;

public record GetExamCommitteeQuery
(
    [Required] Guid Id
) : IRequest<Result<ExamCommitteeResponse>>;