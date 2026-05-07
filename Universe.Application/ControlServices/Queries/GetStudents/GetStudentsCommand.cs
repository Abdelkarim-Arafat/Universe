using Universe.Application.ControlServices.Dtos;


namespace Universe.Application.ControlServices.Queries.GetStudents;

public record GetStudentsCommand
(
    Guid AcademicProgramId,
    Guid CourseOfferingId,
    int? GroupNumber,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StudentInformationResponse>>>;

public record GetStudentsRequest
(
    [Required] Guid AcademicProgramId,
    [Required] Guid CourseOfferingId,
               int? GroupNumber
) : IRequest<Result<PaginationList<StudentInformationResponse>>>;
