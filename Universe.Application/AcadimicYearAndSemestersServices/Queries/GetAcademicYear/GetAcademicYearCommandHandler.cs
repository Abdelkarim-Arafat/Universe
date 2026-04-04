using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicYearAndSemestersServices.Dtos;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAcademicYear;

public class GetAcademicYearCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAcademicYearCommand , Result<AcademicYearResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<AcademicYearResponse>> Handle(GetAcademicYearCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicYearRepository
            .GetByIdWithSemestersAsync(request.Id, cancellationToken) is not { } year
            ) return Result.Failure<AcademicYearResponse>(AcademicYearErrors.NotFound);

        return Result.Success(year.Adapt<AcademicYearResponse>());
    }
}
