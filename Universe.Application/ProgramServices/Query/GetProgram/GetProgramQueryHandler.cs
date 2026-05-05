using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcademicProgram;

namespace Universe.Application.AcademicProgramServices.Query.GetAcademicProgram;

public class GetAcademicProgramQueryHandler (
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetAcademicProgramQuery , Result<AcademicProgramResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<AcademicProgramResponse>> Handle(GetAcademicProgramQuery request, CancellationToken cancellationToken)
    {
        var academicProgram = await _cacheService.GetOrCreateAsync(
            key: AcademicProgramCacheKeys.ById(request.Id),
            factory: async () => await _unitOfWork.AcademicProgramRepository
                .GetByIdAsync(request.Id, cancellationToken),
            cancellationToken: cancellationToken
        );

        if(academicProgram is null)
            return Result.Failure<AcademicProgramResponse>(AcademicProgramErrors.NotFound);

        var response = academicProgram.Adapt<AcademicProgramResponse>();

        return Result.Success(response);
    }
}
