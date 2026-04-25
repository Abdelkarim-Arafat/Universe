using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Commands.UpdateGrade;

public class UpdateGradeCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<UpdateGradeCommand, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GradeResponse>> Handle(UpdateGradeCommand command, CancellationToken cancellationToken = default)
    {
        var grade = await _unitOfWork.GradeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (grade is null)
            return Result.Failure<GradeResponse>(GradeErrors.NotFound);

        var isGradeWithOverLabExist = await _unitOfWork.GradeRepository
            .CheckOverLabedScoresAsync
            (command.MinScore, command.MaxScore, grade.Id, grade.AcademicProgramId, cancellationToken)
            || await _unitOfWork.GradeRepository
            .CheckOverLabedPointsAsync
            (command.MinGradePoint, command.MaxGradePoint, grade.Id, grade.AcademicProgramId, cancellationToken);

        if (isGradeWithOverLabExist)
            return Result.Failure<GradeResponse>(GradeErrors.InvalidScores);

        grade.Name = command.Name;
        grade.Code = command.Code;
        grade.MinScore = command.MinScore;
        grade.MaxScore = command.MaxScore;
        grade.MaxGradePoint = command.MaxGradePoint;
        grade.MinGradePoint = command.MinGradePoint;

        _unitOfWork.Repository<Grade>().Update(grade);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(grade.Adapt<GradeResponse>());
    }
}
