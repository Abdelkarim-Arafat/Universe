using Universe.Core.Contracts.Course;

namespace Universe.Application.CourseServices.Query.GetAllCourses;

public class GetCoursesQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<GetCoursesQuery, Result<PaginationList<CourseResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<CourseResponse>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var filter = request.filter;

        var cacheKey = CourseCacheKeys.List(
            request.CollegeId,
            filter.SearchValue,
            filter.SortColumn,
            filter.SortDirection,
            filter.PageNumber,
            filter.PageSize);

        var tags = CourseCacheKeys.Tags(request.CollegeId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<Course>()
                    .GetQueryable()
                    .Where(x => x.CollegeId == request.CollegeId)
                    .ApplySearch(filter.SearchValue, x => x.Name , x => x.Code)
                    .Select(x => new CourseResponse (
                        x.Id.ToString(),
                        x.Name,
                        x.Code
                    ));

                return await PaginationList<CourseResponse>
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );
        return Result.Success(response);
    }
}