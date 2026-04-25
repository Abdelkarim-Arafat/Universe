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
            .GetStudentAssessmentIncludingCourseAssessmentAsync(command.StudentId, command.CourseAssessmentId, cancellationToken);

        if(studentAssessment == null)
            return Result.Failure<UpsertDegreeResponse>(StudentAssessmentErrors.NotFound);

        if(command.Degree > studentAssessment.CourseOfferingAssessment.MaxScore)
            return Result.Failure<UpsertDegreeResponse>(StudentAssessmentErrors.AssessmentDegreeExceedsMaxScore);

        studentAssessment.degree = command.Degree;

        var TotalDegree = await _unitOfWork.StudentAssessmentRepository
         .GetStudentDegreeInCourseAsync(command.StudentId, CourseOfferingId.Value, cancellationToken);

        var enrollment = await _unitOfWork.EnrollmentRepository
            .GetEnrollmentByStudentIdAndCourseOfferingIdAsync(command.StudentId, CourseOfferingId.Value, cancellationToken);

        if (enrollment == null)
            return Result.Failure<UpsertDegreeResponse>(EnrollmentErrors.NotFound);

        enrollment.Status = TotalDegree >= enrollment.CourseOffering.SuccessPercentage
            ? Core.Enums.EnrollmentStatus.Passed
            : Core.Enums.EnrollmentStatus.Failed;

        _unitOfWork.Repository<StudentAssessment>().Update(studentAssessment);
        _unitOfWork.Repository<Enrollment>().Update(enrollment);

        await _unitOfWork.CompleteAsync(cancellationToken);

        var isProgramExist = await _unitOfWork.AcademicProgramRepository
           .IsExistAsync(command.AcademicProgramId, cancellationToken);

        if (!isProgramExist)
            return Result.Failure<UpsertDegreeResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        var letterGrade = await _unitOfWork.GradeRepository
            .GetLetterGradeByTotalDegree(command.AcademicProgramId, TotalDegree, cancellationToken);

        return Result.Success(new UpsertDegreeResponse(TotalDegree, letterGrade));
    }
}
