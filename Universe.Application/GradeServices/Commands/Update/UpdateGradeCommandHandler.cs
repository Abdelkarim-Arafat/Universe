namespace Universe.Application.GradeServices.Commands.Update;

public class UpdateGradeCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    : IRequestHandler<UpdateGradeCommand, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<GradeResponse>> Handle(UpdateGradeCommand command, CancellationToken cancellationToken = default)
    {
      
        var grade = await _unitOfWork.GradeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (grade is null)
            return Result.Failure<GradeResponse>(GradeErrors.NotFound);

        var isScoresOverlapped = await _unitOfWork.GradeRepository.CheckOverLappedScoresAsync(
            command.MinScore,
            command.MaxScore,
            grade.Id,
            grade.AcademicProgramId,
            cancellationToken);

        var isGradePointsOverlapped = await _unitOfWork.GradeRepository.CheckOverLappedPointsAsync(
                command.MinGradePoint,
                command.MaxGradePoint,
                grade.Id,
                grade.AcademicProgramId,
                cancellationToken);

        if (isScoresOverlapped || isGradePointsOverlapped)
        {
            return Result.Failure<GradeResponse>(GradeErrors.InvalidScores);
        }
        

        command.Adapt(grade);

        _unitOfWork.Repository<Grade>().Update(grade);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(GradeCacheKeys.Tags(grade.AcademicProgramId), cancellationToken);

        return Result.Success(grade.Adapt<GradeResponse>());
    }
}
