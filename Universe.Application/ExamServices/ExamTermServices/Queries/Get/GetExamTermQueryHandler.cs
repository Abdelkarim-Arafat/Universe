
namespace Universe.Application.ExamServices.ExamTermServices.Queries.Get;

public class GetExamTermQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetExamTermQuery, Result<ExamTermResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ExamTermResponse>> Handle(GetExamTermQuery request, CancellationToken cancellationToken)
    {
        var examTerm = await _unitOfWork.ExamRepository.GetExamTermByIdAsync(request.Id, cancellationToken);
        if (examTerm == null)
            return Result.Failure<ExamTermResponse>(ExamErrors.ExamTermNotFound);

        var response = examTerm.Adapt<ExamTermResponse>();
        return Result.Success(response);
    }
}