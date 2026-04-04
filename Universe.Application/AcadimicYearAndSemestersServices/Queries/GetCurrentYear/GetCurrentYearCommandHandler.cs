using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicYearAndSemestersServices.Dtos;
using Universe.Application.AcadimicYearAndSemestersServices.Dtos;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentYear;

public class GetCurrentYearCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCurrentYearCommand, Result<CurrentAcademicYearResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CurrentAcademicYearResponse>> Handle(GetCurrentYearCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CollegeRepository.CheckCollegeIsExistAsync(request.CollegeId) is false)
            return Result.Failure<CurrentAcademicYearResponse>(CollegeErrors.NotFound);

        if(await _unitOfWork.AcademicYearRepository
            .GetCurrentYearAsync(request.CollegeId, cancellationToken) is not { } currentYear
            ) return Result.Failure<CurrentAcademicYearResponse>(AcademicYearErrors.NotFound);

        return Result.Success(currentYear.Adapt<CurrentAcademicYearResponse>());
    }
}
