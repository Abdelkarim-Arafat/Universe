using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Commands.UpdateStudentGraduationDetails;

internal class UpdateStudentGraduationDetailsCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateStudentGraduationDetailsCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateStudentGraduationDetailsCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.UserRepository
            .GetStudentByIdAsync(request.Id , cancellationToken) is not { } student
            ) return Result.Failure<GraduationDetailsResponse>(StudentErrors.DisabledUser);

        request.Adapt(student);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
