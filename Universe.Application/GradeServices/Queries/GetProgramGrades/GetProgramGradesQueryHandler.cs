using Universe.Application.GradeServices.Queries.GetProgramGrades;

public class GetProgramGradesQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<GetProgramGradesQuery, Result<PaginationList<GradeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<GradeResponse>>> Handle(GetProgramGradesQuery request, CancellationToken cancellationToken = default)
    {
        var isAcademicProgramExist = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken);

        if (!isAcademicProgramExist)
            return Result.Failure<PaginationList<GradeResponse>>(AcademicProgramErrors.NotFound);

        var filter = request.Filter;

        var cacheKey = GradeCacheKeys.List(
            request.AcademicProgramId,
            filter.SearchValue,
            filter.SortColumn,
            filter.SortDirection,
            filter.PageNumber,
            filter.PageSize
        );


        var tags = GradeCacheKeys.Tags(request.AcademicProgramId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<Grade>()
                    .GetQueryable()
                    .AsNoTracking() 
                    .Where(grade => grade.AcademicProgramId == request.AcademicProgramId && !grade.IsDeleted);

                if (!string.IsNullOrEmpty(filter.SearchValue))
                {
                    query = query.Where(grade =>
                        grade.Name.Contains(filter.SearchValue) ||
                        grade.Code.Contains(filter.SearchValue));
                }

                if (!string.IsNullOrEmpty(filter.SortColumn))
                    query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

                var projection = query.Select(x => new GradeResponse(
                    x.Id,
                    x.Name,
                    x.Code,
                    x.MinScore,
                    x.MaxScore,
                    x.MinGradePoint,
                    x.MaxGradePoint
                ));

                return await PaginationList<GradeResponse>
                    .CreateAsync(projection, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}