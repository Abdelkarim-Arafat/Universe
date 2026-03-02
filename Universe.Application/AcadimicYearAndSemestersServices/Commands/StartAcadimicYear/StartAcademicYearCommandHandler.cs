
using Universe.Application.AcademicYearAndSemestersServices.Dtos;

namespace Universe.Application.AcademicYearAndSemestersServices.Commands.StartAcademicYear;

internal class StartAcademicYearCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<StartAcademicYearCommand, Result<AcademicYearResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AcademicYearResponse>> Handle(StartAcademicYearCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.CollegeRepository.CheckCollegeIsExistAsync(request.CollegeId) is false)
            return Result.Failure<AcademicYearResponse>(CollegeErrors.NotFound);

        var semesters = request.Semesters.OrderBy(s => s.StartDate).ToList();

        for (int i = 0; i < semesters.Count; i++) 
        {
            if (i > 0 && (int)semesters[i].TermType < (int)semesters[i - 1].TermType)
                return Result.Failure<AcademicYearResponse>(SemesterErrors.OverLabedDateTime);

            for (int j = i + 1; j < semesters.Count; j++)
            {
                if (semesters[i].StartDate <= semesters[j].StartDate && semesters[i].EndDate >= semesters[j].StartDate)
                    return Result.Failure<AcademicYearResponse>(SemesterErrors.OverLabedDateTime);
            }
        }

        var academicYear = new AcademicYear {
            CollegeId = request.CollegeId,
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        foreach (var semester in semesters)
        {
            academicYear.Semesters.Add(new Semester
            {
                Name = semester.TermType,
                StartDate = semester.StartDate,
                EndDate = semester.EndDate,
            });
        }
        await _unitOfWork.Repository<AcademicYear>().AddAsync(academicYear , cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(new AcademicYearResponse
        (
            academicYear.Id.ToString(),
            academicYear.Name,
            academicYear.StartDate,
            academicYear.EndDate,
            academicYear.Semesters.Select(s => new SemesterResponse
            (
                s.Id.ToString(),
                s.Name,
                s.StartDate,
                s.EndDate
            )).ToList()
        ));
    }
}
