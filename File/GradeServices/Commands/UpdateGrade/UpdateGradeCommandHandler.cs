namespace Universe.Application.GradeServices.Commands.UpdateGrade;

public class UpdateGradeCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<UpdateGradeCommand, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitofwork = unitOfWork;

    public async Task<Result<GradeResponse>> Handle(UpdateGradeCommand command, CancellationToken cancellationToken = default)
    {

        var check = await _unitofwork.GradeRepository
            .CheckOverLabedScoresAsync(command.MinScore, command.MaxScore, command.Id, cancellationToken);

        if (check.IsFailure)
            return Result.Failure<GradeResponse>(check.Error);


        var result = await _unitofwork.GradeRepository.GetByIdAsync(command.Id);

        if (result.IsFailure)
            return Result.Failure<GradeResponse>(result.Error);

        var grade = result.Value;

        grade.Name = command.Name;
        grade.Code = command.Code;
        grade.MinScore = command.MinScore;
        grade.MaxScore = command.MaxScore;

        _unitofwork.Repository<Grade>().Update(grade, cancellationToken);
        try
        {
            await _unitofwork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {

            return Result.Failure<GradeResponse>(
                new Error("DatabaseError", ex.Message, StatusCodes.Status409Conflict));
        }

        return Result.Success(grade.Adapt<GradeResponse>());
    }
}
