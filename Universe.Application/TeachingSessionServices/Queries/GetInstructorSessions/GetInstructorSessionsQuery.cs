using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.TeachingSession;
using Universe.Core.Enums;

namespace Universe.Application.TeachingSessionServices.Queries.GetInstructorSessions;

public record GetInstructorSessionsQuery(
    [Required] Guid ProgramId,
    [Required] Guid AcademicYearId,
    [Required] TermType TermType
) : IRequest<Result<IReadOnlyList<SessionResponse>>>;