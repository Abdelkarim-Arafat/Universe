namespace Universe.Application.StudentServices.Queries.GetPreviousQualificationData;


public class GetPreviousQualificationDataQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetPreviousQualificationDataQuery, Result<PreviousQualificationResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PreviousQualificationResponse>> Handle(GetPreviousQualificationDataQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentPreviousQualificationAsync(request.StudentId, cancellationToken) is not { } qualificationResponse
            ) return Result.Failure<PreviousQualificationResponse>(StudentErrors.UserNotFound);

        return Result.Success(qualificationResponse);
    }
}