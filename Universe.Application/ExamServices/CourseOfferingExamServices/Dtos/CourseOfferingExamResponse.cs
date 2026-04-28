using Universe.Application.ExamServices.CourseOfferingExamServices.Dtos;

namespace Universe.Application.CourseOfferingExamServices.Dtos;

public record CourseOfferingExamResponse
(
     Guid Id,
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime,
     PaginationList<CourseExamCommitteesResponse> CourseCommittees
);