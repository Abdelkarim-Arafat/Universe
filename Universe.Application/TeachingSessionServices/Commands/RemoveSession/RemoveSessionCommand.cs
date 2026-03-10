using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.TeachingSessionServices.Commands.RemoveSession;

public record RemoveSessionCommand(
    [Required]Guid SessionId,
    [Required]Guid CourseOfferingId
) : IRequest<Result>;