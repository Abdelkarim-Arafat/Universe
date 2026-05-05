namespace Universe.Application.LevelServices.Commands.Remove;

public class RemoveLevelCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<RemoveLevelCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(RemoveLevelCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.LevelRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } level
            ) return Result.Failure(LevelErrors.NotFound);
            
        _unitOfWork.Repository<Level>().DeletePermanently(level);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(LevelCacheKeys.ById(request.Id), cancellationToken);
        await _cacheService.RemoveByTagAsync(LevelCacheKeys.Tags(request.ProgramId), cancellationToken);

        return Result.Success();
    }
}