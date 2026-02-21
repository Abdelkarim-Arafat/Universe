namespace Universe.Application.GradeServices.Queries.GetCollegeGrades;

public class GetCollegeGradesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCollegeGradesQuery, Result<List<GradeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<GradeResponse>>> Handle(GetCollegeGradesQuery request, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.GradeRepository.GetCollegeGradesAsync(request.CollegeId, cancellationToken);
        if (result.IsFailure)
            return Result.Failure<List<GradeResponse>>(result.Error);

        var grades = result.Value.Adapt<List<GradeResponse>>();

        return Result.Success(grades);
    }
}
