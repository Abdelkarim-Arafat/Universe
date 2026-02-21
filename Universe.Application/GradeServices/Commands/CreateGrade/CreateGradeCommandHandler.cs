using Universe.Application.GradeServices.GradeDtos;

namespace Universe.Application.GradeServices.Commands.CreateGrade;

public class CreateGradeCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<CreateGradeCommand, Result<GradeResponse>>
{
    private readonly IUnitOfWork _unitofWork = unitOfWork;

    public async Task<Result<GradeResponse>> Handle(CreateGradeCommand command, CancellationToken cancellationToken = default)
    {
        var isCollegeExist = await _unitofWork.CollegeRepository.CheckCollegeIsExistAsync(command.CollegeId, cancellationToken);
        if(!isCollegeExist)
            return Result.Failure<GradeResponse>(CollegeErrors.NotFound);

        var isGradeWithOverLabExist = await _unitofWork.GradeRepository
            .CheckOverLabedScoresAsync(command.MinScore, command.MaxScore, cancellationToken);
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
