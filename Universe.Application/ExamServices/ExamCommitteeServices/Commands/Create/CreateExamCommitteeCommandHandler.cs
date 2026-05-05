using Universe.Application.ExamCommitteeServices.Dtos;

namespace Universe.Application.ExamCommitteeServices.Commands.Create;

public class CreateExamCommitteeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateExamCommitteeCommand, Result<ExamCommitteeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ExamCommitteeResponse>> Handle(CreateExamCommitteeCommand request, CancellationToken cancellationToken)
    {
        var room = await _unitOfWork.RoomRepository.GetByIdAsync(request.RoomId, cancellationToken);

        if (room == null)
            return Result.Failure<ExamCommitteeResponse>(RoomErrors.RoomNotFound);

        var IsExistCommitteeWithSameNumber = await _unitOfWork.ExamRepository
            .IsExistCommitteeWithSameNumberAsync(null, request.ExamTermId, request.CommitteeNumber, cancellationToken);

        if (IsExistCommitteeWithSameNumber)
            return Result.Failure<ExamCommitteeResponse>(ExamErrors.SameCommitteeNumber);

        var roomCapacity = room.Capacity;

        if (roomCapacity < request.MaxCapacity)
            return Result.Failure<ExamCommitteeResponse>(RoomErrors.UnValidCapacity);

        var examCommittee = request.Adapt<ExamCommittee>();

        await _unitOfWork.Repository<ExamCommittee>().AddAsync(examCommittee, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(examCommittee.Adapt<ExamCommitteeResponse>());
    }
}