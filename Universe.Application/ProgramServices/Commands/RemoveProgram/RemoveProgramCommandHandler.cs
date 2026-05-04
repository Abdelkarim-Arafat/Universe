
using System.Runtime.InteropServices;
using Universe.Core.Contracts.AcademicProgram;

namespace Universe.Application.AcademicProgramServices.Commands.RemoveAcademicProgram;

public class RemoveAcademicProgramCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<RemoveAcademicProgramCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(RemoveAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CollegeRepository
            .IsExistAsync(request.CollegeId, cancellationToken) is false)
            return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.NotFound);

        var cacheKey = AcademicProgramCacheKeys.ById(request.Id);

        var academicProgram = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () => await _unitOfWork.AcademicProgramRepository
                .GetByIdAsync(request.Id, cancellationToken),
            cancellationToken: cancellationToken
        );

        if (academicProgram is null)
            return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.NotFound);

        _unitOfWork.Repository<AcademicProgram>().SoftDelete(academicProgram);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        await _cacheService.RemoveByTagAsync(AcademicProgramCacheKeys.Tags(request.CollegeId), cancellationToken);

        return Result.Success();
    }
}
