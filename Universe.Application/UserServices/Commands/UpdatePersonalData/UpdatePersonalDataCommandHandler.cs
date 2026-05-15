using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Commands.UpdatePersonalData;

public class UpdatePersonalDataCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<UpdatePersonalDataCommand, Result<PersonalDataResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

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

        request.Adapt(student);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(StudentCacheKeys.Tags(request.ProgramId), cancellationToken);

        return Result.Success(student.Adapt<PersonalDataResponse>());
    }
}
