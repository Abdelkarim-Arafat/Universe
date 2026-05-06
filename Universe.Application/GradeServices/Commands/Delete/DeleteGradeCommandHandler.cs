namespace Universe.Application.GradeServices.Commands.Delete;

public class DeleteGradeCommandHandler
    (IUnitOfWork unitOfWork,
     ICacheService cacheService) : IRequestHandler<DeleteGradeCommand, Result>
{
    private readonly IUnitOfWork _unitofwork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(DeleteGradeCommand command, CancellationToken cancellationToken = default)
    {

        var grade = await _unitofwork.GradeRepository.GetByIdAsync(command.Id, cancellationToken);
        if (grade is null)
            return Result.Failure(GradeErrors.NotFound);

        var programId = grade.AcademicProgramId;

        _unitofwork.Repository<Grade>().DeletePermanently(grade);

        await _unitofwork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(GradeCacheKeys.Tags(programId), cancellationToken);

        return Result.Success();
    }
}