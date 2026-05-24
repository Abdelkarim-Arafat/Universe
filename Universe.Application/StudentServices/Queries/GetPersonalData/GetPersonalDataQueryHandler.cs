namespace Universe.Application.StudentServices.Queries.GetPersonalData;

public class GetPersonalDataQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetPersonalDataQuery, Result<PersonalDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PersonalDataResponse>> Handle(GetPersonalDataQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentPersonalDataAsync(request.StudentId, cancellationToken) is not { } personalDataResponse
            ) return Result.Failure<PersonalDataResponse>(StudentErrors.UserNotFound);

        return Result.Success(personalDataResponse);
    }
}
