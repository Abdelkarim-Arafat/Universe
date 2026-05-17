using Universe.Core.Contracts.StudyLoadByLevel;

namespace Universe.Application.StudyLoadByLevelServices.Queries.GetAll;

public class GetStudyLoadByLevelsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<GetStudyLoadByLevelsQuery, Result<PaginationList<StudyLoadByLevelResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<StudyLoadByLevelResponse>>> Handle(GetStudyLoadByLevelsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = StudyLoadByLevelCacheKeys.List (
            request.ProgramId,
            filter.SearchValue,
            filter.SortColumn,
            filter.SortDirection,
            filter.PageNumber,
            filter.PageSize
        );

        var tags = StudyLoadByLevelCacheKeys.Tags(request.ProgramId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<StudyLoadByLevel>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(studyLoad => studyLoad.AcademicProgramId == request.ProgramId)
                    .ApplySearch(filter.SearchValue, x => x.Level.Name)
                    .Select(studyLoad => new StudyLoadByLevelResponse(
                        studyLoad.Id,
                        studyLoad.SemesterType,
                        studyLoad.Level.Name,
                        studyLoad.LevelId,
                        studyLoad.MinHours,
                        studyLoad.MaxHours
                    ));

                return await PaginationList<StudyLoadByLevelResponse>
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}