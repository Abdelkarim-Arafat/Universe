
namespace Universe.Application.UserServices.Commands.ChangeStudentProgram;

public class ChangeStudentProgramCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeStudentProgramCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangeStudentProgramCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.UserRepository
            .GetStudentByIdAsync(request.StudentId, cancellationToken) is not { } student
            ) return Result.Failure(StudentErrors.UserNotFound);

        if(!(await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.ProgramId, cancellationToken))
            ) return Result.Failure(AcademicProgramErrors.NotFound);

        var currentProgram = await _unitOfWork.AcademicProgramRepository
                                .GetCurrentStudentAcademicProgramAsync(request.StudentId, cancellationToken);

        if(currentProgram is null) return Result.Failure(AcademicProgramErrors.NotFound);

        if (currentProgram.AcademicProgramId == request.ProgramId) return Result.Success();

        currentProgram.Currently = false;
        currentProgram.EndDate = DateOnly.FromDateTime(DateTime.UtcNow);

        var studentProgram = await _unitOfWork.AcademicProgramRepository
                        .GetStudentAcademicProgramAsync(request.ProgramId, student.Id, cancellationToken);

        if (studentProgram == null)
        {
            studentProgram = new StudentAcademicProgram
            {
                StudentId = student.Id,
                AcademicProgramId = request.ProgramId,
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            };
            await _unitOfWork.Repository<StudentAcademicProgram>().AddAsync(studentProgram , cancellationToken);
        }
        else
        {
            studentProgram.Currently = true;
            studentProgram.EndDate = null;
        }

        await _unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success();
    }
}
