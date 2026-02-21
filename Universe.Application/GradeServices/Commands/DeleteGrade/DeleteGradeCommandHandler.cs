using Universe.Application.GradeServices.Commands.CreateGrade;

namespace Universe.Application.GradeServices.Commands.DeleteGrade;

public class DeleteGradeCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<DeleteGradeCommand, Result>
{
    private readonly IUnitOfWork _unitofwork = unitOfWork;

    public async Task<Result> Handle(DeleteGradeCommand command, CancellationToken cancellationToken = default)
    {

        var grade = await _unitofwork.GradeRepository.GetByIdAsync(command.Id);
        if (grade is null)
            return Result.Failure(GradeErrors.NotFound);

        _unitofwork.Repository<Grade>().SoftDelete(grade);

        _unitofwork.Repository<Grade>().Update(grade);
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