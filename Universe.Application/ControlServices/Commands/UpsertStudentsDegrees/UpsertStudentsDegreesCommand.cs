using Universe.Application.ControlServices.Dtos;

namespace Universe.Application.ControlServices.Commands.UpsertStudentsDegrees;

public record UpsertStudentsDegreesCommand
(       [Required] Guid AcademicProgramId,
        [Required] Guid CourseOfferingId,
       List<StudentDegrees> StudentsDegrees
) : IRequest<Result<UpsertDegreesResponse>>;

public record StudentDegrees
(
      [Required] Guid StudentId,
     List<StudentAssessmentValue> StudentDegreeValues
);
public record StudentAssessmentValue
(
    [Required] Guid CourseAssessmentId,
    [Required] Guid StudentAssessmentId,
    decimal? Value
);
