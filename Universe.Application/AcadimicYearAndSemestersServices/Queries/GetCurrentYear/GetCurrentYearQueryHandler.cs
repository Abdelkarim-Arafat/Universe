using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcadimicYearAndSemesters;


namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentYear;

public class GetCurrentYearQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCurrentYearQuery, Result<AcademicYearResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AcademicYearResponse>> Handle(GetCurrentYearQuery request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.CollegeRepository.IsExistAsync(request.CollegeId))
            return Result.Failure<AcademicYearResponse>(CollegeErrors.NotFound);

        if(await _unitOfWork.AcademicYearRepository
            .GetCurrentYearAsync(request.CollegeId, cancellationToken) is not { } currentYear
            ) return Result.Failure<AcademicYearResponse>(AcademicYearErrors.NotFound);

        return Result.Success(currentYear);
    }
}
