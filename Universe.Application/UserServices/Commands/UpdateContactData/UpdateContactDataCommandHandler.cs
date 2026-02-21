using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;
using Universe.Core.Interfaces;

namespace Universe.Application.UserServices.Commands.UpdateContactData;

internal class UpdateContactDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateContactDataCommand, Result<ContactDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ContactDataResponse>> Handle(UpdateContactDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentByIdAsync(request.UserId, cancellationToken) is not { } student)
            return Result.Failure<ContactDataResponse>(StudentErrors.UserNotFound);

        student.Adapt(request);
        _unitOfWork.Repository<Student>().Update(student);

        return Result.Success(student.Adapt<ContactDataResponse>());
    }
}
