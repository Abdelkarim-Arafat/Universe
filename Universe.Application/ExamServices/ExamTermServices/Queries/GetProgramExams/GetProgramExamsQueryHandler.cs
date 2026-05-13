
namespace Universe.Application.ExamServices.ExamTermServices.Queries.GetProgramExams;

public class GetProgramExamsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService)
    : IRequestHandler<GetProgramExamsQuery, Result<PaginationList<ExamTermResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<ExamTermResponse>>> Handle(GetProgramExamsQuery request, CancellationToken cancellationToken)
    {
        var IsSemesterExist = await _unitOfWork.AcademicYearRepository
            .IsExistSemesterAsync(request.SemesterId, cancellationToken);

        if (!IsSemesterExist)
            return Result.Failure<PaginationList<ExamTermResponse>>(SemesterErrors.NotFound);

        var IsProgramExists = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken);

        if (!IsProgramExists)
            return Result.Failure<PaginationList<ExamTermResponse>>(AcademicProgramErrors.NotFound);

        var query = _unitOfWork.Repository<ExamTerm>().GetQueryable()
            .Where(exam => exam.AcademicProgramId == request.AcademicProgramId
            && exam.SemesterId == request.SemesterId
            && !exam.IsDeleted);

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        
        var source = query.Select(x => x.Adapt<ExamTermResponse>());

        var cacheKey = ExamTermCacheKeys.List(request.AcademicProgramId, filter.SearchValue, filter.SortColumn, filter.SortDirection, filter.PageNumber, filter.PageSize);
        var tags = ExamTermCacheKeys.Tags(request.AcademicProgramId);

        var response = await _cacheService.GetOrCreateAsync(
           key: cacheKey,
           factory: async () => await PaginationList<ExamTermResponse>.CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken),
           cancellationToken: cancellationToken,
           tags: tags
       );

        return Result.Success(response);
    }
}