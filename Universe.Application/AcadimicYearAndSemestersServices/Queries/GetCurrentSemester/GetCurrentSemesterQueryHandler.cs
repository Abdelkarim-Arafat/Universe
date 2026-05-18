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

        var currentSemester = await _unitOfWork.AcademicYearRepository
            .GetCurrentSemesterAsync(request.AcademicYearId , cancellationToken);

        if(currentSemester is null) 
            currentSemester = await _unitOfWork.AcademicYearRepository
                .GetLastSeenSemesterAsync(request.AcademicYearId , cancellationToken);

        return Result.Success(currentSemester);
    }
}
