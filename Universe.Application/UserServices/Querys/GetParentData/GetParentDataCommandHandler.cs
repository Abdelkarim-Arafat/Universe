using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.Querys.GetParentData;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetParentData;


public class GetParentDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParentDataCommand, Result<ParentDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ParentDataResponse>> Handle(GetParentDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.GetStudentByIdAsync(request.StudentId, cancellationToken)
            is not { } student) return Result.Failure<ParentDataResponse>(StudentErrors.NotFound);

        return Result.Success(student.ParentInfo.Adapt<ParentDataResponse>());
    }
}
