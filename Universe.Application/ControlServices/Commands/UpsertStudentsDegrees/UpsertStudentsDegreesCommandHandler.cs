using Universe.Application.ControlServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.ControlServices.Commands.UpsertStudentsDegrees;

public class UpsertStudentsDegreesCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpsertStudentsDegreesCommand, Result<UpsertDegreesResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    // eror لو طالب مش موجود او تقيم مش موجود
    public async Task<Result<UpsertDegreesResponse>> Handle(UpsertStudentsDegreesCommand command, CancellationToken cancellationToken)
    {

        var isProgramExist = await _unitOfWork.AcademicProgramRepository.IsExistAsync(command.AcademicProgramId, cancellationToken);
        if (!isProgramExist)
            return Result.Failure<UpsertDegreesResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        var CourseOffering = await _unitOfWork.CourseOfferingRepository
            .GetByIdIncludingEnrollmentsAsync(command.CourseOfferingId, cancellationToken);

        if (CourseOffering == null)
            return Result.Failure<UpsertDegreesResponse>(CourseOfferingErrors.NotFound);

        //var IsOpenForControl = await _unitOfWork.CourseOfferingRepository
        //    .IsOpenForControlAsync(command.CourseOfferingId, cancellationToken);
        
        //if (!IsOpenForControl)
        //    return Result.Failure<UpsertDegreesResponse>(CourseOfferingErrors.NotOpenForControl);
       

        var StudentsIds = command.StudentsDegrees
        .Select(x => x.StudentId)
        .ToList();

        var Assessments = await _unitOfWork.UserRepository.GetStudentsAssessmentsAsync(StudentsIds, cancellationToken);

        var degreesDictionary = command.StudentsDegrees
            .SelectMany(s => s.StudentDegreeValues)
                  .ToDictionary(
                   v => v.StudentAssessmentId,
                   v => v.Value
                 );

        var UpdatedAssessemets = new List<StudentAssessment>();
        var UpdatedEnrollments = new List<Enrollment>();

        foreach (var assessment in Assessments)
        {

            if (!degreesDictionary.TryGetValue(assessment.Id, out var newValue))
                return Result.Failure<UpsertDegreesResponse>
                    (StudentAssessmentErrors.NotFound);

            if (!newValue.HasValue)
                return Result.Failure<UpsertDegreesResponse>
                    (StudentAssessmentErrors.AssessmentWithoutDegree);

            if(newValue > assessment.CourseOfferingAssessment.MaxScore)
                return Result.Failure<UpsertDegreesResponse>
                    (StudentAssessmentErrors.AssessmentDegreeExceedsMaxScore);

            assessment.degree = newValue!.Value;
            UpdatedAssessemets.Add(assessment);       
        }

        var letterDegrees = await _unitOfWork.GradeRepository
            .GetProgramGradesAsync(command.AcademicProgramId, cancellationToken);

        var courseEnrollments = CourseOffering.Enrollments.ToList();

        var updatedScores = Assessments
            .GroupBy(a => a.StudentId)
            .Select(group =>
            {
                var totalDegree = group.Sum(x => x.degree);

                var letter = letterDegrees
                    .FirstOrDefault(lg => totalDegree >= lg.MinScore && totalDegree <= lg.MaxScore)
                    ?.Code ?? "-";

                var studentEnrollment = courseEnrollments.FirstOrDefault(e => e.StudentId == group.Key);
                if (studentEnrollment != null)
                {
 
                    decimal passScore = CourseOffering.SuccessPercentage;

                    studentEnrollment.Status = totalDegree >= passScore
                        ? EnrollmentStatus.Passed
                        : EnrollmentStatus.Failed;
                    UpdatedEnrollments.Add(studentEnrollment);
                }

                return new UpdatedStudentScore(
                    group.Key,
                    totalDegree!.Value,
                    letter!,
                    group.Select(x => new StudentDegreeValue(x.CourseOfferingAssessmentId, x.Id, x.degree)).ToList()
                );
            })
            .ToList();

        foreach(var updatedAssessment in UpdatedAssessemets)
            _unitOfWork.Repository<StudentAssessment>().Update(updatedAssessment);
      
        foreach(var updatedEnrollment in UpdatedEnrollments)
            _unitOfWork.Repository<Enrollment>().Update(updatedEnrollment);
        
        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Failure<UpsertDegreesResponse>
                (new Error("DatabaseError", "An error occurred while updating the database.", StatusCodes.Status409Conflict));
        }

        return Result.Success(new UpsertDegreesResponse(updatedScores));
    }
}
