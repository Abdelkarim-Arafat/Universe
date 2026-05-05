using Universe.Core.Contracts.Level;

namespace Universe.Application.LevelServices.Queries.GetAcademicProgramLevels;

public class GetAcademicProgramLevelsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<GetAcademicProgramLevelsQuery, Result<PaginationList<LevelResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<LevelResponse>>> Handle(GetAcademicProgramLevelsQuery request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.ProgramId, cancellationToken)
           ) return Result.Failure<PaginationList<LevelResponse>>(AcademicProgramErrors.NotFound);
             
        var filter = request.Filter;

        var cacheKey = LevelCacheKeys.List (
            request.ProgramId,
            filter.SearchValue,
            filter.SortColumn,
            filter.SortDirection,
            filter.PageNumber,
            filter.PageSize);

        var tags = LevelCacheKeys.Tags(request.ProgramId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<Level>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(l => l.AcademicProgramId == request.ProgramId)
                    .ApplySearch(filter.SearchValue, x => x.Name)
                    .Select(x => new LevelResponse(
                        x.Id,
                        x.Name,
                        x.MinHours,
                        x.MaxHours
                    ));

                return await PaginationList<LevelResponse>
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );
        return Result.Success(response);
    }
}