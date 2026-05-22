namespace Universe.Application.ControlServices.Commands.ToggleAnnounceResult;

public class ToggleAnnounceResultCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<ToggleAnnounceResultCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ToggleAnnounceResultCommand request, CancellationToken cancellationToken)
    {
        var isProgramExist = await _unitOfWork.AcademicProgramRepository.IsExistAsync(request.ProgramId, cancellationToken);

        if (!isProgramExist)
            return Result.Failure(AcademicProgramErrors.NotFound);

        var Semester = await _unitOfWork.AcademicYearRepository.GetSemesterByIdAsync(request.SemesterId, cancellationToken);

        if (Semester == null)
            return Result.Failure(SemesterErrors.NotFound);

        if (Semester.IsResultAnnounced)
        {
            Semester.IsResultAnnounced = false;
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }

        var hasStudentsWithMissingGrades = await _unitOfWork.EnrollmentRepository
            .HasStudentsWithMissingGradesAsync(request.ProgramId, request.SemesterId, cancellationToken);

        if (hasStudentsWithMissingGrades)
            return Result.Failure(SemesterErrors.StudentsWithMissingGrades);

        Semester.IsResultAnnounced = true;
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
