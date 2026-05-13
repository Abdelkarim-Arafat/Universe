

namespace Universe.Application.ControlServices.Commands.ToggleCourseOfferingControl;

public class ToggleCourseOfferingControlCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<ToggleCourseOfferingControlCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(ToggleCourseOfferingControlCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CourseOfferingRepository
            .GetByIdAsync(request.CourseOfferingId, cancellationToken) is not { } course
            ) return Result.Failure(CourseOfferingErrors.NotFound);

        course.IsOpenForControl = !course.IsOpenForControl;

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(ControlCacheKeys.CourseOfferingsStatistics(course.AcademicProgramId, course.SemesterId) , cancellationToken);

        return Result.Success();
    }
}
