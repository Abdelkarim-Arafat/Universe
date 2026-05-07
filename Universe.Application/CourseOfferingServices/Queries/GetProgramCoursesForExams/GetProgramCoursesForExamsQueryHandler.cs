using Universe.Core.Contracts.CourseOffering;
using Universe.Application.CourseOfferingServices.Query.GetProgramCoursesForExams;

namespace Universe.Application.CourseOfferingServices.Queries.GetProgramCoursesForExams;

public class GetProgramCoursesForExamsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<GetProgramCoursesForExamsQuery, Result<PaginationList<CourseOfferingForExamsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<CourseOfferingForExamsResponse>>> Handle(
        GetProgramCoursesForExamsQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken)
            ) return Result.Failure<PaginationList<CourseOfferingForExamsResponse>>(AcademicProgramErrors.NotFound);

        if (!await _unitOfWork.AcademicYearRepository
            .IsSemesterExistAsync(request.SemesterId, cancellationToken)
            ) return Result.Failure<PaginationList<CourseOfferingForExamsResponse>>(SemesterErrors.NotFound);

        var filter = request.Filter;

        var cacheKey = CourseOfferingCacheKeys.ProgramCoursesForExams(
            request.AcademicProgramId,
            request.SemesterId,
            filter.SearchValue,
            filter.SortColumn,
            filter.SortDirection,
            filter.PageNumber,
            filter.PageSize);

        var tags = CourseOfferingCacheKeys.Tags(request.AcademicProgramId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<CourseOffering>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(x =>
                        !x.IsDeleted &&
                        x.AcademicProgramId == request.AcademicProgramId &&
                        x.SemesterId == request.SemesterId)
                    .ApplySearch(filter.SearchValue, x => x.Course.Name, x => x.Course.Code)
                    .Select(x => new CourseOfferingForExamsResponse(
                        x.Id,
                        x.Course.Name,
                        x.Course.Code,
                        x.Enrollments.Count(e => !e.IsDeleted)
                    ));

                return await PaginationList<CourseOfferingForExamsResponse>
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}