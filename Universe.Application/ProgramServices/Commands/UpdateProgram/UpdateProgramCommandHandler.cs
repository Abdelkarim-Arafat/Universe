using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicProgramServices.AcademicProgramDtos;

namespace Universe.Application.AcademicProgramServices.Commands.UpdateAcademicProgram;

public class UpdateAcademicProgramCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateAcademicProgramCommand, Result<AcademicProgramResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AcademicProgramResponse>> Handle(UpdateAcademicProgramCommand request, CancellationToken cancellationToken)
    {

        if (await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.CollegeId, request.Name, request.Code, request.Id, cancellationToken))
        {
            return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.AcademicProgramAlreadyExists);
        }
             

        var AcademicProgram = await _unitOfWork.AcademicProgramRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if(AcademicProgram is null) return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        request.Adapt(AcademicProgram);

        _unitOfWork.Repository<AcademicProgram>().Update(AcademicProgram);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(AcademicProgram.Adapt<AcademicProgramResponse>());
    }
}
