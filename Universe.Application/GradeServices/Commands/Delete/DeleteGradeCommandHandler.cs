using Universe.Application.GradeServices.Commands.Create;

namespace Universe.Application.GradeServices.Commands.Delete;

public class DeleteGradeCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<DeleteGradeCommand, Result>
{
    private readonly IUnitOfWork _unitofwork = unitOfWork;

    public async Task<Result> Handle(DeleteGradeCommand command, CancellationToken cancellationToken = default)
    {

        var grade = await _unitofwork.GradeRepository.GetByIdAsync(command.Id, cancellationToken);
        if (grade is null)
            return Result.Failure(GradeErrors.NotFound);

        _unitofwork.Repository<Grade>().DeletePermanently(grade);

        await _unitofwork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}