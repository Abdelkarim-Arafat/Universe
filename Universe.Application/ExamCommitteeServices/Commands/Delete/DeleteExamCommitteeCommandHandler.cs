namespace Universe.Application.ExamCommitteeServices.Commands.Delete;

public class DeleteExamCommitteeCommandHandler
    (IUnitOfWork unitOfWork,
    ICacheService cacheService) : IRequestHandler<DeleteExamCommitteeCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(DeleteExamCommitteeCommand request, CancellationToken cancellationToken)
    {
        var examCommittee = await _unitOfWork.ExamRepository.GetExamCommitteeByIdAsync(request.Id, cancellationToken);
       
        if (examCommittee == null)
            return Result.Failure(ExamErrors.ExamCommitteeNotFound);

        var examTermId = examCommittee.ExamTermId;

        _unitOfWork.Repository<ExamCommittee>().DeletePermanently(examCommittee);
        await _unitOfWork.CompleteAsync(cancellationToken);

        var tags = ExamCommitteeCacheKeys.Tags(examTermId);
        await _cacheService.RemoveByTagAsync(tags, cancellationToken);

        return Result.Success();
    }
}