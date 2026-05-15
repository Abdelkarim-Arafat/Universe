using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.Commands.UpdateFamilyData;
using Universe.Core.Contracts.User;

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
            return Result.Failure<PreviousQualificationResponse>(StudentErrors.NotFound);

        request.Adapt(student.PreviousQualification);
        _unitOfWork.Repository<Student>().Update(student);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(student.PreviousQualification.Adapt<PreviousQualificationResponse>());
    }
}
