using Org.BouncyCastle.Asn1.Ocsp;
using Universe.Core.Contracts.Level;
namespace Universe.Application.LevelServices.Commands.CreateLevel;

public class CreateLevelCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<CreateLevelCommand, Result<LevelResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<LevelResponse>> Handle(CreateLevelCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken)
           ) return Result.Failure<LevelResponse>(AcademicProgramErrors.NotFound);

        if(await _unitOfWork.LevelRepository
            .CheckOverLabedHoursAsync(request.MinHours,
                    request.MaxHours, request.AcademicProgramId, cancellationToken)
            ) return Result.Failure<LevelResponse>(LevelErrors.InvalidHours);

        var level = request.Adapt<Level>();

        await _unitOfWork.Repository<Level>().AddAsync(level, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(LevelCacheKeys.Tags(request.AcademicProgramId), cancellationToken);

        var response = await _cacheService.GetOrCreateAsync(
            key: LevelCacheKeys.ById(level.Id),
            factory: async() => level.Adapt<LevelResponse>(),
            cancellationToken: cancellationToken
        );

        return Result.Success(response);
    }
}
