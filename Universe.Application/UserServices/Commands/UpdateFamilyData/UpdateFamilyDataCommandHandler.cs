using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Commands.UpdateFamilyData;

internal class UpdateFamilyDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateFamilyDataCommand, Result<FamilyDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<FamilyDataResponse>> Handle(UpdateFamilyDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentByIdAsync(request.UserId, cancellationToken) is not { } student)
            return Result.Failure<FamilyDataResponse>(StudentErrors.UserNotFound);

        student.Adapt(request);
        _unitOfWork.Repository<Student>().Update(student);

        return Result.Success(student.Adapt<FamilyDataResponse>());
    }
}
