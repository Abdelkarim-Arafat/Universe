using Universe.Application.ControlServices.Dtos;

namespace Universe.Application.ControlServices.Commands.UpsertStudentDegree;

public record UpsertStudentDegreeCommand(
      [Required] Guid AcademicProgramId,
      [Required] Guid StudentId,
      [Required] Guid CourseAssessmentId,
      decimal Degree

) : IRequest<Result<UpsertDegreeResponse>>;
