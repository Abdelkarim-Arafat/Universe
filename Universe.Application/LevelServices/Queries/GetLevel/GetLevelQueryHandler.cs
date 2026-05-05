using Universe.Core.Contracts.Level;

namespace Universe.Application.LevelServices.Queries.GetLevel;

public class GetLevelQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetLevelQuery, Result<LevelResponse>>
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<LevelResponse>> Handle(GetLevelQuery request, CancellationToken cancellationToken = default)
    {
        var response = await _cacheService.GetOrCreateAsync(
            key: LevelCacheKeys.ById(request.Id),
            factory: async () => {
                var level = await unitOfWork.LevelRepository
                        .GetByIdAsync(request.Id, cancellationToken);

                return level.Adapt<LevelResponse>();
            },
            cancellationToken: cancellationToken
        );

        if (response is null)
            return Result.Failure<LevelResponse>(LevelErrors.NotFound);

        return Result.Success(response);
    }
}
