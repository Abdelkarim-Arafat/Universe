using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicProgramServices.AcademicProgramDtos;

namespace Universe.Application.AcademicProgramServices.Commands.AddAcademicProgram;

public class AddAcademicProgramCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddAcademicProgramCommand, Result<AcademicProgramResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AcademicProgramResponse>> Handle(AddAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.CollegeId, request.Name, request.Code , null ,cancellationToken);

        if (isExist) return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.AcademicProgramAlreadyExists);

        var AcademicProgram = request.Adapt<AcademicProgram>();

        await _unitOfWork.Repository<AcademicProgram>().AddAsync(AcademicProgram, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(AcademicProgram.Adapt<AcademicProgramResponse>());
    }
}
