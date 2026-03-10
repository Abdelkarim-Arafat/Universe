using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.TeachingSessionServices.SessionDtos;

namespace Universe.Application.TeachingSessionServices.Commands.AssignCourseToSession;

public class AssignCourseToSessionCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AssignCourseToSessionCommand, Result<SessionResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<SessionResponse>> Handle(AssignCourseToSessionCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.SessionRepository
            .GetByIdAsync(request.SessionId, cancellationToken) is not { } session
            ) return Result.Failure<SessionResponse>(SessionErrors.NotFound);

        session.CourseOfferingSessions.Add(new CourseOfferingSession
        {
            TeachingSessionId = request.SessionId,
            CourseOfferingId = request.CourseOfferingId,
        });

        _unitOfWork.Repository<TeachingSession>().Update(session);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(session.Adapt<SessionResponse>());
    }
}
