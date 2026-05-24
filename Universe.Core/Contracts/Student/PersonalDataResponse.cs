using Universe.Core.Enums;

namespace Universe.Core.Contracts.Student;

public record PersonalDataResponse(
    string Name,
    string StudentCode,
    string NationalIdOrPassport,
    Religion? Religion,
    Gender? Gender,
    DateOnly? DateOfBirth,
    MaritalStatus? MaritalStatus,
    string PlaceOfBirth,
    string Nationality
);