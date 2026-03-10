using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Commands.UpdateFamilyData;

public class UpdateParentDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateParentDataCommand, Result<ParentDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ParentDataResponse>> Handle(UpdateParentDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentByIdAsync(request.StudentId, cancellationToken) is not { } student)
            return Result.Failure<ParentDataResponse>(StudentErrors.UserNotFound);

        student.Adapt(request);
        _unitOfWork.Repository<Student>().Update(student);

        return Result.Success(student.Adapt<ParentDataResponse>());
    }
}
