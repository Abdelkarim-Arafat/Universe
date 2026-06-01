namespace Universe.Application.StudentServices.Commands.ChangeStudentProgram;

public class ChangeStudentProgramCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<ChangeStudentProgramCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(ChangeStudentProgramCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository
            .GetStudentByIdAsync(request.StudentId, cancellationToken) is not { } student
            ) return Result.Failure(StudentErrors.UserNotFound);

        if (!(await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.NewProgramId, cancellationToken))
            ) return Result.Failure(AcademicProgramErrors.NotFound);

        var currentProgram = await _unitOfWork.AcademicProgramRepository
                                .GetCurrentStudentAcademicProgramAsync(request.StudentId, cancellationToken);

        if (currentProgram is null) return Result.Failure(AcademicProgramErrors.NotFound);

        if (currentProgram.AcademicProgramId == request.NewProgramId) return Result.Success();

        currentProgram.Currently = false;
        currentProgram.EndDate = DateOnly.FromDateTime(DateTime.UtcNow);

        var studentProgram = await _unitOfWork.AcademicProgramRepository
                        .GetStudentAcademicProgramAsync(request.NewProgramId, student.Id, cancellationToken);

        if (studentProgram == null)
        {
            studentProgram = new StudentAcademicProgram
            {
                StudentId = student.Id,
                AcademicProgramId = request.NewProgramId,
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            };
            await _unitOfWork.Repository<StudentAcademicProgram>().AddAsync(studentProgram, cancellationToken);
        }
        else
        {
            studentProgram.Currently = true;
            studentProgram.EndDate = null;
        }

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(StudentCacheKeys.ProgramTag(currentProgram.AcademicProgramId), cancellationToken);
        await _cacheService.RemoveByTagAsync(StudentCacheKeys.ProgramTag(request.NewProgramId), cancellationToken);

        return Result.Success();
    }
}
