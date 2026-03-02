using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicProgramServices.AcademicProgramDtos;
using Universe.Application.AcademicProgramServices.Query.GetAcademicProgram;

namespace Universe.Application.AcademicProgramServices.Query.GetAcademicProgram;

public class GetAcademicProgramCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAcademicProgramCommand , Result<AcademicProgramResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AcademicProgramResponse>> Handle(GetAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        var AcademicProgram = await _unitOfWork.AcademicProgramRepository.GetByIdAsync(request.Id, cancellationToken);

        return AcademicProgram is not null
            ? Result.Success(AcademicProgram.Adapt<AcademicProgramResponse>())
            : Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.AcademicProgramNotFound);
    }
}
