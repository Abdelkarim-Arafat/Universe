using Universe.Application.ExamCommitteeServices.Dtos;
namespace Universe.Application.ExamCommitteeServices.Queries.Get;

public record GetAllExamCommitteesQuery
(
    FilterRequest Filter,
    [Required] Guid ExamTermId
) : IRequest<Result<PaginationList<ExamCommitteeResponse>>>;