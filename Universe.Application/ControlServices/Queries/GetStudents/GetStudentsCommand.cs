using Universe.Application.ControlServices.Dtos;

namespace Universe.Application.ControlServices.Queries.GetStudents;

public record GetStudentsCommand
(   [Required] Guid AcademicProgramId,
    [Required] Guid CourseOfferingId,
               int GroupNumber,
               string? StudentCodeOrName,
               FilterRequest Filter
) : IRequest<Result<StudentsDegreesResponse>>;
