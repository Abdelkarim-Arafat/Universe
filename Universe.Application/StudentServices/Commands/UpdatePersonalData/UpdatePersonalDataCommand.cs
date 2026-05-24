using Universe.Core.Enums;

namespace Universe.Application.StudentServices.Commands.UpdatePersonalData;

public record UpdatePersonalDataCommand(
    [Required] Guid CollegeId,
    [Required] Guid StudentId,
    [Required] Guid ProgramId,
    string Name,
    string StudentCode,
    string NationalIdOrPassport,
    Religion Religion,
    Gender Gender,
    MaritalStatus MaritalStatus,
    DateOnly? DateOfBirth,
    string PlaceOfBirth,
    string Nationality
) : IRequest<Result<PersonalDataResponse>>;