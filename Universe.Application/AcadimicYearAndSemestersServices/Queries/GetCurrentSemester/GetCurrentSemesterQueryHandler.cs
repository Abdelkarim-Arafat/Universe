using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcadimicYearAndSemesters;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentSemester;

public class GetCurrentSemesterQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCurrentSemesterQuery , Result<SemesterResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<SemesterResponse>> Handle(GetCurrentSemesterQuery request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.AcademicYearRepository
            .IsExistAsync(request.AcademicYearId, cancellationToken)
            ) return Result.Failure<SemesterResponse>(AcademicYearErrors.NotFound);

        if(await _unitOfWork.AcademicYearRepository
            .GetCurrentSemesterAsync(request.AcademicYearId , cancellationToken) is not { } currentSemester
            ) return Result.Failure<SemesterResponse>(SemesterErrors.NotFound);

        return Result.Success(currentSemester);
    }
}
