using Org.BouncyCastle.Asn1.Ocsp;
using Universe.Core.Contracts.Level;

namespace Universe.Application.LevelServices.Commands.Update;

public class UpdateLevelCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<UpdateLevelCommand, Result<LevelResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<LevelResponse>> Handle(UpdateLevelCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.LevelRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } level
            ) return Result.Failure<LevelResponse>(LevelErrors.NotFound);

        if(await _unitOfWork.LevelRepository
            .CheckOverLabedHoursAsync(request.MinHours, request.MaxHours,
                level.Id, level.AcademicProgramId, cancellationToken)
            ) return Result.Failure<LevelResponse>(LevelErrors.InvalidHours);

        level.Name = request.Name;
        level.MinHours = request.MinHours;
        level.MaxHours = request.MaxHours;

        _unitOfWork.Repository<Level>().Update(level);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(LevelCacheKeys.Tags(request.ProgramId), cancellationToken);
        await _cacheService.RemoveAsync(LevelCacheKeys.ById(level.Id), cancellationToken);

        var response = await _cacheService.GetOrCreateAsync(
            key: LevelCacheKeys.ById(level.Id),
            factory: async () => level.Adapt<LevelResponse>(),
            cancellationToken: cancellationToken
        );

        return Result.Success(response);
    }
}
