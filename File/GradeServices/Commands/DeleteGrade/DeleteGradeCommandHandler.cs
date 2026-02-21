using Universe.Application.GradeServices.Commands.CreateGrade;

namespace Universe.Application.GradeServices.Commands.DeleteGrade;

public class DeleteGradeCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<DeleteGradeCommand, Result>
{
    private readonly IUnitOfWork _unitofwork = unitOfWork;

    public async Task<Result> Handle(DeleteGradeCommand command, CancellationToken cancellationToken = default)
    {

        var result = await _unitofwork.GradeRepository.GetByIdAsync(command.Id);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        var grade = result.Value;

        _unitofwork.Repository<Grade>().SoftDelete(grade);

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
        return Result.Success();
    }
}