using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Queries.GetInstructorSessions;

internal class GetInstructorSessionsQueryHandler(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetInstructorSessionsQuery, Result<IReadOnlyList<SessionResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<IReadOnlyList<SessionResponse>>> Handle(GetInstructorSessionsQuery request, CancellationToken cancellationToken)
    {
        var instructorId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (await _unitOfWork.UserRepository
            .IsUserExistAsync(instructorId, cancellationToken) is false
            ) return Result.Failure<IReadOnlyList<SessionResponse>>(AuthErrors.UserNotFound);

        if (await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.ProgramId, cancellationToken) is false
            ) return Result.Failure<IReadOnlyList<SessionResponse>>(AcademicProgramErrors.NotFound);

        if(await _unitOfWork.AcademicYearRepository
            .GetSemesterByTypeAsync(request.AcademicYearId , request.TermType, cancellationToken) is not { } semester
            ) return Result.Failure<IReadOnlyList<SessionResponse>>(AcademicYearErrors.NotFound);

        var sessions = await _unitOfWork.SessionRepository
                .GetInstructorSessionsAsync(request.ProgramId, instructorId, semester.Id, cancellationToken);

        return Result.Success(sessions);
    }
}
