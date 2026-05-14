using Universe.Application.ControlServices.Dtos;
namespace Universe.Application.ControlServices.Commands.UpsertStudentDegree;

public class UpsertStudentDegreeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpsertStudentDegreeCommand, Result<UpsertDegreeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<UpsertDegreeResponse>> Handle(UpsertStudentDegreeCommand command, CancellationToken cancellationToken)
    {
        var isStudentExist = await _unitOfWork.UserRepository
            .IsUserExistAsync(command.StudentId, cancellationToken);

        if (!isStudentExist)
            return Result.Failure<UpsertDegreeResponse>(StudentErrors.NotFound);

        var isProgramExist = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(command.AcademicProgramId,cancellationToken);

        if (!isProgramExist)
            return Result.Failure<UpsertDegreeResponse>(AcademicProgramErrors.NotFound);

        var courseData = await _unitOfWork.CourseOfferingRepository
            .GetCourseOfferingDataByAssessmentIdAsync(command.CourseAssessmentId, cancellationToken);

        if (courseData == null)
            return Result.Failure<UpsertDegreeResponse>(CourseOfferingErrors.NotFoundAssessment);

        if (!courseData.IsCourseOpenForControl)
            return Result.Failure<UpsertDegreeResponse>(CourseOfferingErrors.NotOpenForControl);

        var assessmentData = await _unitOfWork.StudentAssessmentRepository
             .GetAssessmentWithMaxScoreAsync(command.StudentId, command.CourseAssessmentId, cancellationToken);

        if (assessmentData == null)
            return Result.Failure<UpsertDegreeResponse>(StudentAssessmentErrors.NotFound);

        bool isExceedingMaxScore = command.Degree > assessmentData.MaxScore;

        if (isExceedingMaxScore)
            return Result.Failure<UpsertDegreeResponse>(StudentAssessmentErrors.AssessmentDegreeExceedsMaxScore);

        var enrollment = await _unitOfWork.EnrollmentRepository
            .GetEnrollmentDataByCourseOfferingIdAsync(courseData.CourseOfferingId, command.StudentId, cancellationToken);

        if (enrollment == null)
            return Result.Failure<UpsertDegreeResponse>(EnrollmentErrors.NotFound);

        var totalDegree = await _unitOfWork.StudentAssessmentRepository
         .GetStudentDegreeInCourseAsync(command.StudentId, courseData.CourseOfferingId, cancellationToken);

        var oldDegree = assessmentData.Assessment!.degree;

        var difference = command.Degree - (oldDegree.HasValue ? oldDegree.Value : 0);

        totalDegree += difference;

        assessmentData.Assessment.degree = command.Degree;

        enrollment.Status = totalDegree >= courseData.SuccessPercentage
            ? Core.Enums.EnrollmentStatus.Passed
            : Core.Enums.EnrollmentStatus.Failed;

        _unitOfWork.Repository<StudentAssessment>().Update(assessmentData.Assessment);
        _unitOfWork.Repository<Enrollment>().Update(enrollment);

        await _unitOfWork.CompleteAsync(cancellationToken);

        var letterGrade = await _unitOfWork.GradeRepository
            .GetLetterGradeByTotalDegree(command.AcademicProgramId, totalDegree, cancellationToken);

        return Result.Success(new UpsertDegreeResponse(totalDegree, letterGrade));
    }
}