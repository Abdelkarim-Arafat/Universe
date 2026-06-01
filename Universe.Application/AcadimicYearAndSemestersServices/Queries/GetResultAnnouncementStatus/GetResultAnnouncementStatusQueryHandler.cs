using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcademicYearAndSemesters;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetResultAnnouncementStatus;


internal class GetResultAnnouncementStatusQueryHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<GetResultAnnouncementStatusQuery, Result<ResultAnnounceStatusResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ResultAnnounceStatusResponse>> Handle(GetResultAnnouncementStatusQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicYearRepository
            .GetResultAnnounceAsync(request.SemesterId, cancellationToken) is not { } result
            ) return Result.Failure<ResultAnnounceStatusResponse>(SemesterErrors.NotFound);

        return Result.Success(result);
    }
}
