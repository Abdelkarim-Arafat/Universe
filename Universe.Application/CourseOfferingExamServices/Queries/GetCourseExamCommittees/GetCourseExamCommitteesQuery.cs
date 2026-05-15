
namespace Universe.Application.CourseOfferingExamServices.Queries.GetCourseExamCommittees;

public record GetCourseExamCommitteesQuery
([Required] Guid id,
            FilterRequest Filter)
    : IRequest<Result<PaginationList<CourseExamCommitteesResponse>>>;
