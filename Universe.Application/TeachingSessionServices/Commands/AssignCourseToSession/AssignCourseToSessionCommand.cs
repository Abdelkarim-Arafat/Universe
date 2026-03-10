using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.TeachingSessionServices.SessionDtos;
using Universe.Infrastructure.Repositories;

namespace Universe.Application.TeachingSessionServices.Commands.AssignCourseToSession;

public record AssignCourseToSessionCommand(
    [Required] Guid SessionId,
    [Required] Guid CourseOfferingId
) : IRequest<Result<SessionResponse>>;