using Org.BouncyCastle.Asn1.Ocsp;
using Universe.Core.Contracts.Level;

namespace Universe.Application.GradeServices.Commands.Create;

public class CreateGraderequestHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<CreateGradeCommand, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<GradeResponse>> Handle(CreateGradeCommand request, CancellationToken cancellationToken = default)
    {

        if (await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken) is false
           ) return Result.Failure<GradeResponse>(AcademicProgramErrors.NotFound);


        var isGradeWithOverLabExist =
                await _unitOfWork.GradeRepository.CheckOverLabedScoresAsync
                    (request.MinScore, request.MaxScore, null, request.AcademicProgramId, cancellationToken)
             || await _unitOfWork.GradeRepository.CheckOverLabedPointsAsync
                    (request.MinGradePoint, request.MaxGradePoint, null, request.AcademicProgramId, cancellationToken);

        if (isGradeWithOverLabExist)
            return Result.Failure<GradeResponse>(GradeErrors.InvalidScores);

         
        var grade = request.Adapt<Grade>();
        await _unitOfWork.Repository<Grade>().AddAsync(grade, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(GradeCacheKeys.Tags(request.AcademicProgramId), cancellationToken);

        return Result.Success(grade.Adapt<GradeResponse>());
    }
}