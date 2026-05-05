

using Universe.Core.Contracts.AcadimicYearAndSemesters;

namespace Universe.Application.AcademicYearAndSemestersServices.Commands.StartAcademicYear;

internal class StartAcademicYearCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
	) : IRequestHandler<StartAcademicYearCommand, Result<AcademicYearWithSemesterResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<AcademicYearWithSemesterResponse>> Handle(StartAcademicYearCommand request, CancellationToken cancellationToken)
    {
        string Name = $"{request.StartDate.Year}-{request.EndDate.Year}";

        if (!await _unitOfWork.CollegeRepository.IsExistAsync(request.CollegeId))
            return Result.Failure<AcademicYearWithSemesterResponse>(CollegeErrors.NotFound);

        if (await _unitOfWork.AcademicYearRepository
            .IsMakeConflictAsync(request.CollegeId, Name, request.StartDate, request.EndDate, default , cancellationToken)
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

        var academicYear = new AcademicYear {
            CollegeId = request.CollegeId,
            Name = Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        foreach (var semester in semesters)
        {
            bool flag = (int)semester.TermType == 1;

            academicYear.Semesters.Add(new Semester
            {
                Name = semester.TermType,
                StartDate = semester.StartDate,
                EndDate = semester.EndDate,
                IsCurrent = flag
            });
        }
        await _unitOfWork.Repository<AcademicYear>().AddAsync(academicYear , cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

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
