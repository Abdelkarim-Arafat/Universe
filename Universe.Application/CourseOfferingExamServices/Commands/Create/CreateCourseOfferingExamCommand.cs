
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
