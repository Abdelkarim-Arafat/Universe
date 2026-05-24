namespace Universe.Application.StudentServices.Queries.GetParentData;


public class GetParentDataQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParentDataQuery, Result<ParentDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ParentDataResponse>> Handle(GetParentDataQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentParentDataAsync(request.StudentId, cancellationToken) is not { } parentResponse
            ) return Result.Failure<ParentDataResponse>(StudentErrors.UserNotFound);

        return Result.Success(parentResponse);
    }
}
