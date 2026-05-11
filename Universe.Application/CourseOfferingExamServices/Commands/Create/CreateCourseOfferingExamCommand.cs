using Universe.Application.CourseOfferingExamServices.Dtos;

namespace Universe.Application.CourseOfferingExamServices.Commands.Create;

public record CreateCourseOfferingExamCommand
(
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime,
   [Required] Guid CourseOfferingId,
   [Required] Guid ExamTermId,
     List<Guid> ExamCommitteesIds
) : IRequest<Result<CourseOfferingExamResponse>>;

public record CreateCourseOfferingExamRequest
(
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime,
     List<Guid> ExamCommitteesIds
);
