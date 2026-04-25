using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Commands.CreateGrade;

public class CreateGradeCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<CreateGradeCommand, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GradeResponse>> Handle(CreateGradeCommand command, CancellationToken cancellationToken = default)
    {
        var isProgramExist = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(command.AcademicProgramId, cancellationToken);

        if(!isProgramExist)
            return Result.Failure<GradeResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        var isGradeWithOverLabExist = await _unitOfWork.GradeRepository
             .CheckOverLabedScoresAsync
             (command.MinScore, command.MaxScore, null, command.AcademicProgramId, cancellationToken)
             || await _unitOfWork.GradeRepository
             .CheckOverLabedPointsAsync
             (command.MinGradePoint, command.MaxGradePoint, null, command.AcademicProgramId, cancellationToken);

        if (isGradeWithOverLabExist)
            return Result.Failure<GradeResponse>(GradeErrors.InvalidScores);

        var grade = command.Adapt<Grade>();

        await _unitOfWork.Repository<Grade>().AddAsync(grade, cancellationToken);
      
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(grade.Adapt<GradeResponse>());
    }
}
