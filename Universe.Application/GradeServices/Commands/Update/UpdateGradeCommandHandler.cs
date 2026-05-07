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

     
        var isGradeWithOverLabExist = await _unitOfWork.GradeRepository
            .CheckOverLabedScoresAsync(command.MinScore, command.MaxScore, grade.Id, grade.AcademicProgramId, cancellationToken)
            || await _unitOfWork.GradeRepository
            .CheckOverLabedPointsAsync(command.MinGradePoint, command.MaxGradePoint, grade.Id, grade.AcademicProgramId, cancellationToken);

        if (isGradeWithOverLabExist)
            return Result.Failure<GradeResponse>(GradeErrors.InvalidScores);

        command.Adapt(grade);

        _unitOfWork.Repository<Grade>().Update(grade);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(GradeCacheKeys.Tags(grade.AcademicProgramId), cancellationToken);

        return Result.Success(grade.Adapt<GradeResponse>());
    }
}
