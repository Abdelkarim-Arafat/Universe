using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Queries.GetGrade;

public class GetGradeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGradeQuery, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GradeResponse>> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
        var grade = await _unitOfWork.GradeRepository.GetByIdAsync(request.Id, cancellationToken);
        if (grade is null)
            return Result.Failure<GradeResponse>(GradeErrors.NotFound);

        return Result.Success(grade.Adapt<GradeResponse>());
    }
}