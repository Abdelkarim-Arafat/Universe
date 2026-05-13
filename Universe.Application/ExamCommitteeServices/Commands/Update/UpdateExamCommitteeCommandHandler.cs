
namespace Universe.Application.ExamCommitteeServices.Commands.Update;

public class UpdateExamCommitteeCommandHandler
    (IUnitOfWork unitOfWork,
    ICacheService cacheService) : IRequestHandler<UpdateExamCommitteeCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(UpdateExamCommitteeCommand request, CancellationToken cancellationToken)
    {
        var examCommittee = await _unitOfWork.ExamRepository.GetExamCommitteeByIdAsync(request.Id, cancellationToken);

        if (examCommittee == null)
            return Result.Failure(ExamErrors.ExamCommitteeNotFound);

        var IsExistCommitteeWithSameNumber = await _unitOfWork.ExamRepository
            .IsExistCommitteeWithSameNumberAsync
            (examCommittee.Id, examCommittee.ExamTermId, request.CommitteeNumber, cancellationToken); 

        if (IsExistCommitteeWithSameNumber)
            return Result.Failure<ExamCommitteeResponse>(ExamErrors.SameCommitteeNumber);

        var roomCapacity = await _unitOfWork.RoomRepository.GetRoomCapacity(examCommittee.RoomId, cancellationToken);

        if (roomCapacity < request.MaxCapacity)
            return Result.Failure<ExamCommitteeResponse>(RoomErrors.UnValidCapacity);

        examCommittee.CommitteeNumber = request.CommitteeNumber;
        examCommittee.MaxCapacity = request.MaxCapacity;

        _unitOfWork.Repository<ExamCommittee>().Update(examCommittee);
        await _unitOfWork.CompleteAsync(cancellationToken);

        var tags = ExamCommitteeCacheKeys.Tags(examCommittee.ExamTermId);
        await _cacheService.RemoveByTagAsync(tags, cancellationToken);

        return Result.Success();
    }
}