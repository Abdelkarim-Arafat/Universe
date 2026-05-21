using Universe.Core.Contracts.CourseOffering;
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
                    .Select(x => new CourseOfferingForExamsResponse(
                        x.Id,
                        x.Course.Name,
                        x.Course.Code,
                        x.Enrollments.Count(e => !e.IsDeleted),
                        x.CourseOfferingExams
                                .Where(coe => !coe.IsDeleted && coe.ExamTermId == request.examTermId)
                                .Select(coe => coe.Id)
                                .FirstOrDefault()
                    ));

                if(!string.IsNullOrWhiteSpace(filter.SearchValue))
                {
                    query = query.Where(x =>
                        x.CouresName.Contains(filter.SearchValue) ||
                        x.CouresCode.Contains(filter.SearchValue));
                }

                return await PaginationList<CourseOfferingForExamsResponse>
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}