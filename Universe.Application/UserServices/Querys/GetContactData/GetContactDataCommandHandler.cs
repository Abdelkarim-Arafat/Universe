using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.Querys.GetContactData;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetContactData;

public class GetContactDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetContactDataCommand, Result<ContactDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<ContactDataResponse>> Handle(GetContactDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.GetStudentByIdAsync(request.StudentId, cancellationToken)
            is not { } student) return Result.Failure<ContactDataResponse>(StudentErrors.UserNotFound);

        return Result.Success(student.ContactInfo.Adapt<ContactDataResponse>());
    }
}