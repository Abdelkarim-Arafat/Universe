using Universe.Core.Contracts.CourseOffering;
using Universe.Application.CourseOfferingServices.Query.GetLevelCourses;

public class GetLevelCoursesQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<GetLevelCoursesQuery, Result<IReadOnlyList<CourseOfferingResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<IReadOnlyList<CourseOfferingResponse>>> Handle(
        GetLevelCoursesQuery request,
        CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicYearRepository
            .GetSemesterByTypeAsync(request.AcademicYearId, request.SemesterType, cancellationToken) is not { } semester
            ) return Result.Failure<IReadOnlyList<CourseOfferingResponse>>(AcademicYearErrors.NotFound);

        var cacheKey = CourseOfferingCacheKeys.LevelCourses(request.LevelId, semester.Id);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () => await _unitOfWork.CourseOfferingRepository
                    .GetLevelCoursesAsync(request.LevelId, semester.Id, cancellationToken),
            cancellationToken: cancellationToken
        );

        return Result.Success(response);
    }
}