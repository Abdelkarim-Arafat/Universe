using Universe.Application.ControlServices.Dtos;

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

        var IsCourseExists = await _unitOfWork.CourseOfferingRepository.IsExistAsync(command.CourseOfferingId, cancellationToken);
        if (!IsCourseExists)
            return Result.Failure<UpsertDegreesResponse>(CourseOfferingErrors.NotFound);

        //var courseAssessments = await _unitOfWork.CourseOfferingRepository
        //    .GetCourseOfferingAssessments(command.CourseOfferingId, cancellationToken);

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
            _unitOfWork.Repository<StudentAssessment>().Update(assessment);
        }

        await _unitOfWork.CompleteAsync(cancellationToken);

        var letterDegrees = await _unitOfWork.GradeRepository
            .GetProgramGradesAsync(command.AcademicProgramId, cancellationToken);

        var updatedScores = Assessments
            .GroupBy(a => a.StudentId)
            .Select(group =>
            {

                var totalDegree = group.Sum(x => x.degree);

                var letter = letterDegrees
                    .FirstOrDefault(lg => totalDegree >= lg.MinScore && totalDegree <= lg.MaxScore)
                    ?.Code ?? "F";

                return new UpdatedStudentScore(
                    group.Key,
                    totalDegree!.Value,
                    letter!,
                    group.Select(x => new StudentDegreeValue(x.CourseOfferingAssessmentId, x.Id, x.degree)).ToList()
                );
            })
            .ToList();

        return Result.Success(new UpsertDegreesResponse(updatedScores));
    }
}
