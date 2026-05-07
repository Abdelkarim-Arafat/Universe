using Universe.Application.CourseOfferingExamServices.Dtos;

namespace Universe.Application.CourseOfferingExamServices.Commands.Update;

public record UpdateCourseOfferingExamCommand
(
    [Required] Guid Id,
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime,
     List<Guid> ExamCommitteesIds
) : IRequest<Result<CourseOfferingExamResponse>>;

public record UpdateCourseOfferingExamRequest
(
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime,
     List<Guid> ExamCommitteesIds
);