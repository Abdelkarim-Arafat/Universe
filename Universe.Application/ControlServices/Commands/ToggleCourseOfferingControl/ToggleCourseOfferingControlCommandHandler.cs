

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
        var courseOffering = await _unitOfWork.CourseOfferingRepository
            .GetByIdAsync(request.CourseOfferingId, cancellationToken);

        if (courseOffering == null)
            return Result.Failure(CourseOfferingErrors.NotFound);

        courseOffering.IsOpenForControl = !courseOffering.IsOpenForControl;

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(ControlCacheKeys.CourseOfferingsStatistics(courseOffering.AcademicProgramId, courseOffering.SemesterId) , cancellationToken);

        return Result.Success();
    }
}
