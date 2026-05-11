using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetMilitaryData;

public class GetMilitaryDataQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMilitaryDataQuery, Result<MilitaryDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<MilitaryDataResponse>> Handle(GetMilitaryDataQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentMilitaryDataAsync(request.StudentId, cancellationToken) is not { } militaryResponse
            ) return Result.Failure<MilitaryDataResponse>(StudentErrors.UserNotFound);

        return Result.Success(militaryResponse);
    }
}
