using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Commands.UpdateMilitaryData;

public class UpdateMilitaryDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateMilitaryDataCommand, Result<MilitaryDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<MilitaryDataResponse>> Handle(UpdateMilitaryDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
           .GetStudentByIdAsync(request.StudentId, cancellationToken) is not { } student)
            return Result.Failure<MilitaryDataResponse>(StudentErrors.UserNotFound);

        student.Adapt(request);
        _unitOfWork.Repository<Student>().Update(student);

        return Result.Success(student.Adapt<MilitaryDataResponse>());
    }
}
