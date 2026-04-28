using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetMilitaryData;

public class GetMilitaryDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMilitaryDataCommand, Result<MilitaryDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<MilitaryDataResponse>> Handle(GetMilitaryDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.GetStudentByIdAsync(request.StudentId, cancellationToken)
            is not { } student) return Result.Failure<MilitaryDataResponse>(StudentErrors.UserNotFound);

        return Result.Success(student.MilitaryInfo.Adapt<MilitaryDataResponse>())!;
    }
}
