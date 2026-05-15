using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.Querys.GetParentData;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetParentData;


public class GetParentDataQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParentDataQuery, Result<ParentDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ParentDataResponse>> Handle(GetParentDataQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentParentDataAsync(request.StudentId, cancellationToken) is not { } parentResponse
            ) return Result.Failure<ParentDataResponse>(StudentErrors.NotFound);

        return Result.Success(parentResponse);
    }
}
