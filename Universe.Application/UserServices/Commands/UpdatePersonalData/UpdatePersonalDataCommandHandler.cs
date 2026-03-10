using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Commands.UpdatePersonalData;

public class UpdatePersonalDataCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdatePersonalDataCommand, Result<PersonalDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PersonalDataResponse>> Handle(UpdatePersonalDataCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentByIdAsync(request.StudentId, cancellationToken) is not { } student)
            return Result.Failure<PersonalDataResponse>(StudentErrors.UserNotFound);

        if (await _unitOfWork.UserRepository
            .IsStudentCodeExistsAsync(request.CollegeId , student.Id, request.StudentCode, cancellationToken))
            return Result.Failure<PersonalDataResponse>(StudentErrors.DuplicateStudentCode);

        if (await _unitOfWork.UserRepository
            .IsStudentNationalIdExistsAsync(request.CollegeId, student.Id , request.NationalIdOrPassport, cancellationToken))
            return Result.Failure<PersonalDataResponse>(StudentErrors.DuplicateNationalIdOrPassport);

        student.Adapt(request);
        _unitOfWork.Repository<Student>().Update(student); 

        return Result.Success(student.Adapt<PersonalDataResponse>());
    }
}
