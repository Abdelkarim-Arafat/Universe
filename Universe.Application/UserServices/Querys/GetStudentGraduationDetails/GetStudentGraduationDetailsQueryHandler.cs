using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetStudentGraduationDetails;

public class GetStudentGraduationDetailsQueryHandler (
    IUnitOfWork unitOfWork
) : IRequestHandler<GetStudentGraduationDetailsQuery, Result<GraduationDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GraduationDetailsResponse>> Handle(
        GetStudentGraduationDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.UserRepository
            .GetStudentGraduationDetailsAsync(request.StudentId, cancellationToken);

        if(response is null) return Result.Failure<GraduationDetailsResponse>(StudentErrors.UserNotFound);

        return Result.Success(response);
    }
}