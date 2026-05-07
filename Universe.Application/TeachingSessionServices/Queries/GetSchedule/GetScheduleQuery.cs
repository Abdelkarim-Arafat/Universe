using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Queries.GetSchedule;

public record GetScheduleQuery(
    [Required] Guid ProgramId,
    [Required] Guid SemesterId
) : IRequest<Result<ScheduleResponse>>;
