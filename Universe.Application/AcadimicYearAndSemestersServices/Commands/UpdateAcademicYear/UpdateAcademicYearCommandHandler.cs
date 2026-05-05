using Universe.Core.Contracts.AcadimicYearAndSemesters;

namespace Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateAcademicYear;

public class UpdateAcademicYearCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<UpdateAcademicYearCommand , Result<AcademicYearWithSemesterResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<AcademicYearWithSemesterResponse>> Handle(UpdateAcademicYearCommand request , CancellationToken cancellationToken)
    {
        string Name = $"{request.StartDate.Year}-{request.EndDate.Year}";

        if (await _unitOfWork.CollegeRepository.IsExistAsync(request.CollegeId) is false)
            return Result.Failure<AcademicYearWithSemesterResponse>(CollegeErrors.NotFound);

        if(await _unitOfWork.AcademicYearRepository
            .GetByIdAsync(request.Id , cancellationToken) is not { } academicYear
            ) return Result.Failure<AcademicYearWithSemesterResponse>(AcademicYearErrors.NotFound);

        if (await _unitOfWork.AcademicYearRepository
            .IsMakeConflictAsync(request.CollegeId, Name , request.StartDate, request.EndDate, request.Id, cancellationToken)
            ) return Result.Failure<AcademicYearWithSemesterResponse>(AcademicYearErrors.MakeConflict);

        var semesters = request.Semesters.OrderBy(s => s.StartDate).ToList();

        for (int i = 0; i < semesters.Count; i++)
        {
            if (i > 0 && (int)semesters[i].TermType < (int)semesters[i - 1].TermType)
                return Result.Failure<AcademicYearWithSemesterResponse>(SemesterErrors.OverLabedDateTime);

            for (int j = i + 1; j < semesters.Count; j++)
            {
                if (semesters[i].StartDate <= semesters[j].StartDate && semesters[i].EndDate >= semesters[j].StartDate)
                    return Result.Failure<AcademicYearWithSemesterResponse>(SemesterErrors.OverLabedDateTime);
            }
        }

        academicYear.Name = Name;
        academicYear.StartDate = request.StartDate;
        academicYear.EndDate = request.EndDate;

        foreach (var semester in semesters)
        {
            var existingSemester = academicYear.Semesters
                .FirstOrDefault(s => s.Id == semester.Id);

            if (existingSemester is not null)
            {
                existingSemester.StartDate = semester.StartDate;
                existingSemester.EndDate = semester.EndDate;
            }
            else return Result.Failure<AcademicYearWithSemesterResponse>(SemesterErrors.NotFound);
        }

        await _unitOfWork.CompleteAsync(cancellationToken);

		await _cacheService.RemoveAsync(AcademicYearCacheKeys.ById(request.Id), cancellationToken);
		await _cacheService.RemoveByTagAsync(AcademicYearCacheKeys.Tags(request.CollegeId), cancellationToken);

	    var response = await _cacheService.GetOrCreateAsync(
			key: AcademicYearCacheKeys.ById(academicYear.Id),
			factory: async () => new AcademicYearWithSemesterResponse(
				academicYear.Id,
				academicYear.Name,
				academicYear.StartDate,
				academicYear.EndDate,
				academicYear.Semesters.Select(s => new SemesterResponse(
					s.Id,
					s.Name,
					s.StartDate,
					s.EndDate
				)).ToList()
			),
			cancellationToken: cancellationToken
		);
		return Result.Success(response);
	}
}
