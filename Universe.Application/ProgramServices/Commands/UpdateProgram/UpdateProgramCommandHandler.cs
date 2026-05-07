using Universe.Core.Contracts.AcademicProgram;

namespace Universe.Application.AcademicProgramServices.Commands.UpdateAcademicProgram;

public class UpdateAcademicProgramCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<UpdateAcademicProgramCommand, Result<AcademicProgramResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<AcademicProgramResponse>> Handle(UpdateAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.CollegeRepository
            .IsExistAsync(request.CollegeId, cancellationToken) is false)
            return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.NotFound);

        if (await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.CollegeId, request.Name, request.Code, request.Id, cancellationToken))
            return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.AlreadyExists);

        if (await _unitOfWork.AcademicProgramRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } academicProgram
            ) return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.NotFound);

        request.Adapt(academicProgram);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveAsync(AcademicProgramCacheKeys.ById(request.Id), cancellationToken);
        await _cacheService.RemoveByTagAsync(AcademicProgramCacheKeys.Tags(request.CollegeId), cancellationToken);

        var response = academicProgram.Adapt<AcademicProgramResponse>();

        return Result.Success(response);
    }
}