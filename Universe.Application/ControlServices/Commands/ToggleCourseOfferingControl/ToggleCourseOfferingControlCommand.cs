
namespace Universe.Application.ControlServices.Commands.ToggleCourseOfferingControl;

public record ToggleCourseOfferingControlCommand(
    [Required] Guid CourseOfferingId
) : IRequest<Result>;