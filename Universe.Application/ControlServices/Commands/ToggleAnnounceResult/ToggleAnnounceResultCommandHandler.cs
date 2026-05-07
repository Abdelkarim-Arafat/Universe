namespace Universe.Application.ControlServices.Commands.ToggleAnnounceResult;

public class ToggleAnnounceResultCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<ToggleAnnounceResultCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ToggleAnnounceResultCommand request, CancellationToken cancellationToken)
    {
        var Semester = await _unitOfWork.AcademicYearRepository.GetSemesterByIdAsync(request.SemesterId, cancellationToken);

        if (Semester == null)
            return Result.Failure(SemesterErrors.NotFound);

        Semester.IsResultAnnounced = !Semester.IsResultAnnounced;

        _unitOfWork.Repository<Semester>().Update(Semester);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
