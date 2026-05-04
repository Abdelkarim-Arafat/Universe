using Universe.Core.Contracts.AcademicProgram;

namespace Universe.Application.AcademicProgramServices.Commands.AddAcademicProgram;

public class AddAcademicProgramCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<AddAcademicProgramCommand, Result<AcademicProgramResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<AcademicProgramResponse>> Handle(AddAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicProgramRepository
           .IsExistAsync(request.CollegeId, request.Name, request.Code, null, cancellationToken)
            ) return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.AlreadyExists);

        var academicProgram = request.Adapt<AcademicProgram>();

        await _cacheService.GetOrCreateAsync (
            key: AcademicProgramCacheKeys.ById(academicProgram.Id),
            factory: async () => academicProgram,
            cancellationToken: cancellationToken
        );

        await _cacheService.RemoveByTagAsync(AcademicProgramCacheKeys.Tags(request.CollegeId), cancellationToken);

        await _unitOfWork.Repository<AcademicProgram>().AddAsync(academicProgram, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        var response = academicProgram.Adapt<AcademicProgramResponse>();

        return Result.Success(response);
    }
}
