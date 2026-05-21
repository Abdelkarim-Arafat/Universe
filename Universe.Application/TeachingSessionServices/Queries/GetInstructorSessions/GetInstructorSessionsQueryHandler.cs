using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Queries.GetInstructorSessions;

internal class GetInstructorSessionsQueryHandler(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetInstructorSessionsQuery, Result<IReadOnlyList<InstructorSessions>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<IReadOnlyList<InstructorSessions>>> Handle(GetInstructorSessionsQuery request, CancellationToken cancellationToken)
    {
        var instructorId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var instructor = await _userManager.FindByIdAsync(instructorId.ToString());

        if (instructor is null) return Result.Failure<IReadOnlyList<InstructorSessions>>(AuthErrors.UserNotFound);

        if (await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.ProgramId, cancellationToken) is false
            ) return Result.Failure<IReadOnlyList<InstructorSessions>>(AcademicProgramErrors.NotFound);

        if(await _unitOfWork.AcademicYearRepository
            .GetCurrentYearAsync(instructor.CollegeId , cancellationToken) is not { } currentYear
            ) return Result.Failure<IReadOnlyList<InstructorSessions>>(AcademicYearErrors.NotFound);

        if(await _unitOfWork.AcademicYearRepository
            .GetCurrentSemesterAsync(currentYear.Id , cancellationToken) is not { } currentSemester
            ) return Result.Failure<IReadOnlyList<InstructorSessions>>(AcademicYearErrors.NotFound);

        var sessions = await _unitOfWork.SessionRepository
                .GetInstructorSessionsAsync(request.ProgramId, instructorId, currentSemester.Id, cancellationToken);

        return Result.Success(sessions);
    }
}
