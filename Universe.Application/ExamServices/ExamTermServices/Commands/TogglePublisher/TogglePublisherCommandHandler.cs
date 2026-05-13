namespace Universe.Application.ExamServices.ExamTermServices.Commands.TogglePublisher;

public class TogglePublisherCommandHandler
     (IUnitOfWork unitOfWork,
      ICacheService cacheService) : IRequestHandler<TogglePublisherCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(TogglePublisherCommand request, CancellationToken cancellationToken)
    {
        var examTerm = await _unitOfWork.ExamRepository.GetExamTermByIdAsync(request.Id, cancellationToken);
        if (examTerm == null)
            return Result.Failure<ExamTermResponse>(ExamErrors.ExamTermNotFound);

        examTerm.IsPublished = !examTerm.IsPublished;

        _unitOfWork.Repository<ExamTerm>().Update(examTerm);
        await _unitOfWork.CompleteAsync(cancellationToken);

        var tags = ExamTermCacheKeys.Tags(examTerm.AcademicProgramId);
        await _cacheService.RemoveByTagAsync(tags, cancellationToken);

        return Result.Success();
    }
}
