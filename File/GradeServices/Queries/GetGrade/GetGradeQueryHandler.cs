using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Queries.GetGrade;

public class GetGradeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGradeQuery, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GradeResponse>> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GradeRepository.GetByIdAsync(request.Id, cancellationToken);
        if (result.IsFailure)
            return Result.Failure<GradeResponse>(result.Error);
        var grade = result.Value.Adapt<GradeResponse>();
        return Result.Success(grade);
    }
}