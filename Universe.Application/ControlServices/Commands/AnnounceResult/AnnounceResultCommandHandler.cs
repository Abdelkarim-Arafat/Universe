using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicYearAndSemestersServices.Commands.StartAcademicYear;
using Universe.Application.AcademicYearAndSemestersServices.Dtos;
using Universe.Application.AcadimicYearAndSemestersServices.Dtos;

namespace Universe.Application.ControlServices.Commands.AnnounceResult;

public class AnnounceResultCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AnnounceResultCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AnnounceResultCommand request, CancellationToken cancellationToken)
    {
        var Semester = await _unitOfWork.AcademicYearRepository.GetSemesterByIdAsync(request.SemesterId, cancellationToken);
        if (Semester == null)
            return Result.Failure(SemesterErrors.NotFound);

        Semester.IsResultAnnounced = true;
        _unitOfWork.Repository<Semester>().Update(Semester);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success();
    }
}
