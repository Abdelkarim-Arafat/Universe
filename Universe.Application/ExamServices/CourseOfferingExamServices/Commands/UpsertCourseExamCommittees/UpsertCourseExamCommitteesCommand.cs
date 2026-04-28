using Universe.Application.ExamServices.CourseOfferingExamServices.Dtos;

namespace Universe.Application.ExamServices.CourseOfferingExamServices.Commands.UpsertCourseExamCommittees;

public record UpsertCourseExamCommitteesCommand
(
    Guid CourseOfferingExamId,
    List<Guid> ExamCommitteesIds,
    FilterRequest Filter
) : IRequest<Result<PaginationList<CourseExamCommitteesResponse>>>;

public record UpsertCourseExamCommitteesRequest
    (List<Guid> ExamCommitteesIds);