using Universe.Application.ControlServices.Dtos;

namespace Universe.Application.ControlServices.Commands.UpsertStudentDegree;

public class UpsertStudentDegreeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpsertStudentDegreeCommand, Result<UpsertDegreeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<UpsertDegreeResponse>> Handle(UpsertStudentDegreeCommand command, CancellationToken cancellationToken)
    {
        var isUserExists = await _unitOfWork.UserRepository
           .UserIsExistAsync(command.StudentId, cancellationToken);

        if (!isUserExists)
            return Result.Failure<UpsertDegreeResponse>(StudentErrors.UserNotFound);

        var CourseOfferingId = await _unitOfWork.CourseOfferingRepository
            .GetIdByCourseAssessmentIdAsync(command.CourseAssessmentId, cancellationToken);

        if (CourseOfferingId == null) 
            return Result.Failure<UpsertDegreeResponse>(CourseOfferingErrors.NotFound);

        var IsOpenForControl = await _unitOfWork.CourseOfferingRepository
            .IsOpenForControlAsync(CourseOfferingId.Value, cancellationToken);
        
        if (!IsOpenForControl)
           return Result.Failure<UpsertDegreeResponse>(CourseOfferingErrors.NotOpenForControl);
       
         var studentAssessment = await _unitOfWork.StudentAssessmentRepository
            .GetStudentAssessmentAsync(command.StudentId, command.CourseAssessmentId, cancellationToken);

        if(studentAssessment == null)
            return Result.Failure<UpsertDegreeResponse>(StudentAssessmentErrors.NotFound);

        studentAssessment.degree = command.Degree;

        _unitOfWork.Repository<StudentAssessment>().Update(studentAssessment);
        await _unitOfWork.CompleteAsync(cancellationToken);


        var isProgramExist = await _unitOfWork.AcademicProgramRepository
           .IsExistAsync(command.AcademicProgramId, cancellationToken);

        if (!isProgramExist)
            return Result.Failure<UpsertDegreeResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        var letterDegrees = await _unitOfWork.GradeRepository
           .GetProgramGradesAsync(command.AcademicProgramId, cancellationToken);

        var TotalDegree = await _unitOfWork.StudentAssessmentRepository
          .GetStudentDegreeInCourseAsync(command.StudentId, CourseOfferingId.Value, cancellationToken);

        var letterDegree = letterDegrees
            .Where(g => TotalDegree >= g.MinScore && TotalDegree <= g.MaxScore)
            .Select(g => g.Code)
            .FirstOrDefault() ?? "-";

        return Result.Success(new UpsertDegreeResponse(TotalDegree, letterDegree));
    }
}
