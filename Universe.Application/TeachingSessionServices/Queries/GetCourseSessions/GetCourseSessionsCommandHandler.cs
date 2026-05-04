using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Queries.GetCourseSessions;

public class GetCourseSessionsCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCourseSessionsCommand, Result<List<SessionResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<SessionResponse>>> Handle(GetCourseSessionsCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CourseOfferingRepository
            .GetByIdAsync(request.CourseOfferingId, cancellationToken) is not { } course
            ) return Result.Failure<List<SessionResponse>>(CourseOfferingErrors.NotFound);

        var response = await _unitOfWork.Repository<CourseOfferingSession>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x => x.CourseOfferingId == request.CourseOfferingId
                && x.TeachingSession.GroupNumber == request.GroupNumber)
            .Select(x => new SessionResponse(
                x.TeachingSession.Id.ToString(),
                x.TeachingSession.StartTime,
                x.TeachingSession.EndTime,
                x.TeachingSession.Type,
                x.TeachingSession.Day,
                x.TeachingSession.InstructorId.ToString(),
                x.TeachingSession.Instructor.Name,
                x.TeachingSession.RoomId.ToString(),
                x.TeachingSession.Room.Name,
                x.TeachingSession.GroupNumber
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(response);
    }
}