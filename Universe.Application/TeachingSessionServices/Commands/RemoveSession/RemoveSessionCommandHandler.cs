namespace Universe.Application.TeachingSessionServices.Commands.RemoveSession;

public class RemoveSessionCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveSessionCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveSessionCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.SessionRepository
            .GetCourseOfferingSessionByIdAsync(request.CourseOfferingId, request.SessionId, cancellationToken)
            is not { } courseOfferingSession) return Result.Failure(SessionErrors.NotFound);

        _unitOfWork.Repository<CourseOfferingSession>().DeletePermanently(courseOfferingSession);

        var hasOtherCourses = await _unitOfWork.SessionRepository
                             .HasOtherCoursesAsync(request.SessionId, request.CourseOfferingId, cancellationToken);

        if(!hasOtherCourses)
        {
            var session = await _unitOfWork.SessionRepository.GetByIdAsync(request.SessionId, cancellationToken);
            if(session != null) _unitOfWork.Repository<TeachingSession>().DeletePermanently(session);
        }

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
