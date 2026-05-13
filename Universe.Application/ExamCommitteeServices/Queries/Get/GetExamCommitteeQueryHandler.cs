namespace Universe.Application.ExamCommitteeServices.Queries.Get;

public class GetExamCommitteeQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetExamCommitteeQuery, Result<ExamCommitteeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<ExamCommitteeResponse>> Handle(GetExamCommitteeQuery request, CancellationToken cancellationToken)
    {
        var examCommittee = await _unitOfWork.ExamRepository.GetExamCommitteeByIdAsync(request.Id, cancellationToken);

        if (examCommittee == null)
            return Result.Failure<ExamCommitteeResponse>(ExamErrors.ExamCommitteeNotFound);

        string place = await _unitOfWork.Repository<ExamCommittee>()
            .GetQueryable()
            .Where(comm => !comm.IsDeleted && comm.Id == examCommittee.Id)
            .Select(comm => $"{comm.Room.RoomNumber} - {comm.Room.Building.Name}")
            .FirstOrDefaultAsync(cancellationToken) ?? "No Place";

        var response = new ExamCommitteeResponse
            (examCommittee.Id,
            examCommittee.MaxCapacity,
            examCommittee.CommitteeNumber,
            place
            );

        return Result.Success(response);
    }
}