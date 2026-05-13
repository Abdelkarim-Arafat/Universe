using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;
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
            .GetStudentByIdAsync(request.StudentId, cancellationToken) is not { } student)
            return Result.Failure<ContactDataResponse>(StudentErrors.NotFound);

        request.Adapt(student.ContactInfo);
        _unitOfWork.Repository<Student>().Update(student);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(student.ContactInfo.Adapt<ContactDataResponse>());
    }
}
