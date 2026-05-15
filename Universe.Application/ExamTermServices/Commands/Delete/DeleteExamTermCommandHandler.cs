
namespace Universe.Application.ExamTermServices.Commands.Delete;

public class DeleteExamTermCommandHandler
    (IUnitOfWork unitOfWork,
     ICacheService cacheService) : IRequestHandler<DeleteExamTermCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(DeleteExamTermCommand command, CancellationToken cancellationToken)
    {
        var examTerm = await _unitOfWork.ExamRepository.GetExamTermByIdAsync(command.Id, cancellationToken);

        if (examTerm == null)
            return Result.Failure<ExamTermResponse>(ExamErrors.ExamTermNotFound);

        var programId = examTerm.AcademicProgramId;
        _unitOfWork.Repository<ExamTerm>().SoftDelete(examTerm);
        await _unitOfWork.CompleteAsync(cancellationToken);

        var tags = ExamTermCacheKeys.Tags(programId);
        await _cacheService.RemoveByTagAsync(tags, cancellationToken);

        return Result.Success();
    }
}