
namespace Universe.Application.CourseServices.Commands.RemoveCoursePrerequisite;

public record RemoveCoursePrerequisiteCommand(
    [Required]Guid CourseId,
    [Required]Guid PreRequisiteId
) : IRequest<Result>;