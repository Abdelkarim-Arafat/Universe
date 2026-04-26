using Universe.Application.ExamCommitteeServices.Dtos;
namespace Universe.Application.ExamCommitteeServices.Queries.Get;

public class GetExamCommitteeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetExamCommitteeQuery, Result<ExamCommitteeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ExamCommitteeResponse>> Handle(GetExamCommitteeQuery request, CancellationToken cancellationToken)
    {
        var examCommittee = await _unitOfWork.ExamRepository.GetExamCommitteeByIdAsync(request.Id, cancellationToken);

        if (examCommittee == null)
            return Result.Failure<ExamCommitteeResponse>(ExamErrors.ExamCommitteeNotFound);

        var response = examCommittee.Adapt<ExamCommitteeResponse>();

        return Result.Success(response);
    }
}