using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Queries.GetInstructorSessions;

public record GetInstructorSessionsQuery(
    [Required] Guid ProgramId
) : IRequest<Result<IReadOnlyList<InstructorSessions>>>;