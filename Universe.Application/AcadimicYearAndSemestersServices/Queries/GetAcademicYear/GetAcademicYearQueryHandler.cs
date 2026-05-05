using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcadimicYearAndSemesters;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAcademicYear;

public class GetAcademicYearQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetAcademicYearQuery , Result<AcademicYearWithSemesterResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<AcademicYearWithSemesterResponse>> Handle(GetAcademicYearQuery request, CancellationToken cancellationToken)
    {
        var yearResponse = await _cacheService.GetOrCreateAsync(
            key: AcademicYearCacheKeys.ById(request.Id),
            factory: async () => await _unitOfWork.AcademicYearRepository
                .GetByIdWithSemestersAsync(request.Id, cancellationToken),
            cancellationToken: cancellationToken
        );

        if(yearResponse is null) 
            return Result.Failure<AcademicYearWithSemesterResponse>(AcademicYearErrors.NotFound);

        return Result.Success(yearResponse);
    }
}
