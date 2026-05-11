using Universe.Core.Contracts.ExamCommittees;

namespace Universe.Application.ExamServices.CourseOfferingExamServices.Queries.GetCourseExamCommittees;

public record GetCourseExamCommitteesQuery
([Required] Guid id,
            FilterRequest Filter)
    : IRequest<Result<PaginationList<CourseExamCommitteesResponse>>>;
