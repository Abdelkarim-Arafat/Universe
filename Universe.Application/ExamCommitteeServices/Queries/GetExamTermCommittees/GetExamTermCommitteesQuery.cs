namespace Universe.Application.ExamCommitteeServices.Queries.GetExamTermCommittees;


public record GetExamTermCommitteesQuery
(
    FilterRequest Filter,
    [Required] Guid ExamTermId
) : IRequest<Result<PaginationList<ExamCommitteeResponse>>>;