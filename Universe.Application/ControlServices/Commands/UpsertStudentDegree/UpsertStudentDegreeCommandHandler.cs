using Universe.Application.ControlServices.Dtos;
namespace Universe.Application.ControlServices.Commands.UpsertStudentDegree;

public class UpsertStudentDegreeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpsertStudentDegreeCommand, Result<UpsertDegreeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<UpsertDegreeResponse>> Handle(UpsertStudentDegreeCommand command, CancellationToken cancellationToken)
    {
        var studentData = await _unitOfWork.StudentAssessmentRepository
            .GetContextForDegreeUpsertAsync(command.StudentId, command.CourseAssessmentId, command.AcademicProgramId, cancellationToken);

        if (studentData == null)
            return Result.Failure<UpsertDegreeResponse>(StudentAssessmentErrors.NotFound);

        if (!studentData.IsAcademicProgramValid)
            return Result.Failure<UpsertDegreeResponse>(AcademicProgramErrors.NotFound);

        if (!studentData.IsCourseOpenForControl)
            return Result.Failure<UpsertDegreeResponse>(CourseOfferingErrors.NotOpenForControl);

        if (studentData.AssessmentToUpdate == null)
            return Result.Failure<UpsertDegreeResponse>(StudentAssessmentErrors.NotFound);

        if (studentData.EnrollmentToUpdate == null)
            return Result.Failure<UpsertDegreeResponse>(EnrollmentErrors.NotFound); // extra

        if (command.Degree > studentData.MaxScore)
            return Result.Failure<UpsertDegreeResponse>(StudentAssessmentErrors.AssessmentDegreeExceedsMaxScore);

        var TotalDegree = await _unitOfWork.StudentAssessmentRepository
         .GetStudentDegreeInCourseAsync(command.StudentId, studentData.CourseOfferingId, cancellationToken);

        var assDegree = studentData.AssessmentToUpdate.degree;
        TotalDegree += command.Degree - (assDegree.HasValue ? assDegree.Value : 0);
        studentData.AssessmentToUpdate.degree = command.Degree;

        studentData.EnrollmentToUpdate.Status = TotalDegree >= studentData.SuccessPercentage
            ? Core.Enums.EnrollmentStatus.Passed
            : Core.Enums.EnrollmentStatus.Failed;

        _unitOfWork.Repository<StudentAssessment>().Update(studentData.AssessmentToUpdate);
        _unitOfWork.Repository<Enrollment>().Update(studentData.EnrollmentToUpdate);

        await _unitOfWork.CompleteAsync(cancellationToken);

        var letterGrade = await _unitOfWork.GradeRepository
            .GetLetterGradeByTotalDegree(command.AcademicProgramId, TotalDegree, cancellationToken);

        return Result.Success(new UpsertDegreeResponse(TotalDegree, letterGrade));
    }
}