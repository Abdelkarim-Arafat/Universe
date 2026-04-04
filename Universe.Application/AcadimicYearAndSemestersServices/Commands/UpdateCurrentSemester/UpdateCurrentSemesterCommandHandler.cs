using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicYearAndSemestersServices.Dtos;
using Universe.Application.AcadimicYearAndSemestersServices.Dtos;

namespace Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateCurrentSemester;

public class UpdateCurrentSemesterCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCurrentSemesterCommand , Result<SemesterResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<SemesterResponse>> Handle(UpdateCurrentSemesterCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicYearRepository
            .GetByIdWithSemestersAsync(request.AcademicYearId, cancellationToken) is not { } year
            ) return Result.Failure<SemesterResponse>(AcademicYearErrors.NotFound);

        foreach (var semester in year.Semesters)
            semester.IsCurrent = semester.Name == request.TermType;

        _unitOfWork.Repository<AcademicYear>().Update(year);
        await _unitOfWork.CompleteAsync(cancellationToken);

        var response = year.Semesters.First(x => x.IsCurrent)
                        .Adapt<SemesterResponse>();

        return Result.Success(response);
    }
}
