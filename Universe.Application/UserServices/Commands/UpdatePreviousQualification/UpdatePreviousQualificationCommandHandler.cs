using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.Commands.UpdateFamilyData;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Commands.UpdatePreviousQualification;

public class UpdatePreviousQualificationCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdatePreviousQualificationCommand, Result<PreviousQualificationResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PreviousQualificationResponse>> Handle(UpdatePreviousQualificationCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentByIdAsync(request.StudentId, cancellationToken) is not { } student)
            return Result.Failure<PreviousQualificationResponse>(StudentErrors.UserNotFound);

        student.Adapt(request);
        _unitOfWork.Repository<Student>().Update(student);

        return Result.Success(student.Adapt<PreviousQualificationResponse>());
    }
}
