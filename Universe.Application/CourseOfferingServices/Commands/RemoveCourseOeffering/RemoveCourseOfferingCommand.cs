using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.CourseOfferingServices.Commands.RemoveCourseOeffering;

public record RemoveCourseOfferingCommand(
    [Required] Guid AcademicProgramId,
    [Required] Guid Id
) : IRequest<Result>;