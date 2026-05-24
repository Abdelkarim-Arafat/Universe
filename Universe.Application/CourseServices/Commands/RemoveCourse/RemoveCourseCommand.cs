namespace Universe.Application.CourseServices.Commands.RemoveCourse;

public record RemoveCourseCommand(
    [Required] Guid CollegeId,
    [Required] Guid Id
) : IRequest<Result>;
