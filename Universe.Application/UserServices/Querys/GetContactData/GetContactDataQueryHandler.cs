using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.Querys.GetContactData;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetContactData;

public class GetContactDataQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetContactDataQuery, Result<ContactDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<ContactDataResponse>> Handle(GetContactDataQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentContactDataAsync(request.StudentId, cancellationToken) is not { } contactResponse
            ) return Result.Failure<ContactDataResponse>(StudentErrors.NotFound);

        return Result.Success(contactResponse);
    }
}