using Universe.Core.Enums;

namespace Universe.Core.Contracts.Student;

public record StudentResponse(
    Guid Id,
    string Name,
    string StudentCode,
    string NationalIdOrPassport,
    Gender? Gender
);