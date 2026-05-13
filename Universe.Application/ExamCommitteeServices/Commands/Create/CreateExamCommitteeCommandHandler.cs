
namespace Universe.Application.ExamCommitteeServices.Commands.Create;

public class CreateExamCommitteeCommandHandler
    (IUnitOfWork unitOfWork,
    ICacheService cacheService) : IRequestHandler<CreateExamCommitteeCommand, Result<ExamCommitteeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<ExamCommitteeResponse>> Handle(CreateExamCommitteeCommand request, CancellationToken cancellationToken)
    {
        var room = await _unitOfWork.RoomRepository.GetByIdAsync(request.RoomId, cancellationToken);

        if (room == null)
            return Result.Failure<ExamCommitteeResponse>(RoomErrors.NotFound);

        if (room.Capacity < request.MaxCapacity)
            return Result.Failure<ExamCommitteeResponse>(RoomErrors.UnValidCapacity);

        var isRoomHasAnotherCommittee = await _unitOfWork.RoomRepository
            .IsRoomHasAnotherCommitteeAsync(request.RoomId, request.ExamTermId, cancellationToken);

        if (isRoomHasAnotherCommittee)
            return Result.Failure<ExamCommitteeResponse>(RoomErrors.AlreadyHasCommittee);

        var isExistExamTerm = await _unitOfWork.ExamRepository
            .IsExistExamTermAsync(request.ExamTermId, cancellationToken);

        if (!isExistExamTerm)
            return Result.Failure<ExamCommitteeResponse>(ExamErrors.ExamTermNotFound);

        var IsExistCommitteeWithSameNumber = await _unitOfWork.ExamRepository
            .IsExistCommitteeWithSameNumberAsync(null, request.ExamTermId, request.CommitteeNumber, cancellationToken);

        if (IsExistCommitteeWithSameNumber)
            return Result.Failure<ExamCommitteeResponse>(ExamErrors.SameCommitteeNumber);

        var examCommittee = request.Adapt<ExamCommittee>();

        var building = await _unitOfWork.BuildingRepository.GetByIdAsync(room.BuildingId, cancellationToken);

        await _unitOfWork.Repository<ExamCommittee>().AddAsync(examCommittee, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        var tags = ExamCommitteeCacheKeys.Tags(request.ExamTermId);
        await _cacheService.RemoveByTagAsync(tags, cancellationToken);

        var response = new ExamCommitteeResponse
        (
            examCommittee.Id,
            examCommittee.MaxCapacity,
            examCommittee.CommitteeNumber,
            $"{room.RoomNumber} - {building!.Name}"
        );

        return Result.Success(response);

    }
}