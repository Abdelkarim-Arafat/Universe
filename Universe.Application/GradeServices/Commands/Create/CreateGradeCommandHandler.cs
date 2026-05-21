using Org.BouncyCastle.Asn1.Ocsp;
using Universe.Core.Contracts.Level;

namespace Universe.Application.GradeServices.Commands.Create;

public class CreateGraderequestHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<CreateGradeCommand, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<GradeResponse>> Handle(CreateGradeCommand command, CancellationToken cancellationToken = default)
    {

        if (await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(command.AcademicProgramId, cancellationToken) is false
           ) return Result.Failure<GradeResponse>(AcademicProgramErrors.NotFound);

        var isScoresOverlapped = await _unitOfWork.GradeRepository.CheckOverLappedScoresAsync(
            command.MinScore,
            command.MaxScore,
            null,
            command.AcademicProgramId,
            cancellationToken);

        var isGradePointsOverlapped = await _unitOfWork.GradeRepository.CheckOverLappedPointsAsync(
                command.MinGradePoint,
                command.MaxGradePoint,
                null,
                command.AcademicProgramId,
                cancellationToken);

        if (isScoresOverlapped || isGradePointsOverlapped)
            return Result.Failure<GradeResponse>(GradeErrors.InvalidScores);
     
        var grade = command.Adapt<Grade>();

        await _unitOfWork.Repository<Grade>().AddAsync(grade, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(GradeCacheKeys.Tags(command.AcademicProgramId), cancellationToken);

        return Result.Success(grade.Adapt<GradeResponse>());
    }
}