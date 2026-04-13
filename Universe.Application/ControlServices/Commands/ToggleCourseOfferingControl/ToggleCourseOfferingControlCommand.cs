using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.ControlServices.Commands.ToggleCourseOfferingControl;

public record ToggleCourseOfferingControlCommand(
    [Required] Guid CourseOfferingId,
    bool IsOpenForControl
) : IRequest<Result>;