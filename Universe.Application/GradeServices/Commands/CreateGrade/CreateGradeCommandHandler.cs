using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Commands.CreateGrade;

public class CreateGradeCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<CreateGradeCommand, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitofWork = unitOfWork;

    public async Task<Result<GradeResponse>> Handle(CreateGradeCommand command, CancellationToken cancellationToken = default)
    {
        var isProgramExist = await _unitofWork.AcademicProgramRepository.IsExistAsync(command.AcademicProgramId, cancellationToken);
        if(!isProgramExist)
            return Result.Failure<GradeResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        var isGradeWithOverLabExist = await _unitofWork.GradeRepository
            .CheckOverLabedScoresAsync(command.MinScore, command.MaxScore, command.AcademicProgramId, cancellationToken);
        if (isGradeWithOverLabExist)
            return Result.Failure<GradeResponse>(GradeErrors.InvalidScores);

        var grade = command.Adapt<Grade>();

        await _unitofWork.Repository<Grade>().AddAsync(grade, cancellationToken);
        try
        {
            await _unitofWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            
            return Result.Failure<GradeResponse>(
                new Error("DatabaseError", ex.Message, StatusCodes.Status409Conflict));
        }
      

        return Result.Success(grade.Adapt<GradeResponse>());

    }
}
