using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicYearAndSemestersServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateAcademicYear;

public class UpdateAcademicYearCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateAcademicYearCommand , Result<AcademicYearResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AcademicYearResponse>> Handle(UpdateAcademicYearCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CollegeRepository.CheckCollegeIsExistAsync(request.CollegeId) is false)
            return Result.Failure<AcademicYearResponse>(CollegeErrors.NotFound);

        if(await _unitOfWork.AcademicYearRepository
            .GetByIdAsync(request.Id , cancellationToken) is not { } academicYear
            ) return Result.Failure<AcademicYearResponse>(AcademicYearErrors.NotFound);

        if (await _unitOfWork.AcademicYearRepository
            .IsMakeConflictAsync(request.CollegeId, request.Name, request.StartDate, request.EndDate, request.Id, cancellationToken)
            ) return Result.Failure<AcademicYearResponse>(AcademicYearErrors.MakeConflict);

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

        academicYear.Name = request.Name;
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
            else return Result.Failure<AcademicYearResponse>(SemesterErrors.NotFound);
        }

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
