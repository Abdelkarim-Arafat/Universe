namespace Universe.Application.StudentServices.Queries.GetContactData;

public class GetContactDataQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetContactDataQuery, Result<ContactDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<ContactDataResponse>> Handle(GetContactDataQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentContactDataAsync(request.StudentId, cancellationToken) is not { } contactResponse
            ) return Result.Failure<ContactDataResponse>(StudentErrors.UserNotFound);

        return Result.Success(contactResponse);
    }
}