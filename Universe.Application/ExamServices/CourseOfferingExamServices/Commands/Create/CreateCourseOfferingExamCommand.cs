using Universe.Application.CourseOfferingExamServices.Dtos;

namespace Universe.Application.CourseOfferingExamServices.Commands.Create;

public record CreateCourseOfferingExamCommand
(
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime,
   [Required] Guid CourseOfferingId,
   [Required] Guid ExamTermId
) : IRequest<Result<CourseOfferingExamResponse>>;