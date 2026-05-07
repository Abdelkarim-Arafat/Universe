namespace Universe.Application.CourseOfferingServices.Commands.RemoveCourseOeffering;

public class RemoveCourseOfferingCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<RemoveCourseOfferingCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(RemoveCourseOfferingCommand request, CancellationToken cancellationToken)
    {
        if ((await _unitOfWork.CourseOfferingRepository
            .GetByIdAsync(request.Id, cancellationToken)) is not { } course
            ) return Result.Failure(CourseOfferingErrors.NotFound);

        _unitOfWork.Repository<CourseOffering>().SoftDelete(course);
        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(CourseOfferingCacheKeys.ById(course.Id), cancellationToken);
        await _cacheService.RemoveAsync(CourseOfferingCacheKeys.LevelCourses(course.LevelId, course.Id), cancellationToken);
        await _cacheService.RemoveByTagAsync(CourseOfferingCacheKeys.Tags(request.AcademicProgramId), cancellationToken);

        return Result.Success();
    }
}
