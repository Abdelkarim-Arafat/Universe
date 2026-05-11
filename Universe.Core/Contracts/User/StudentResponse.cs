using Universe.Core.Enums;

namespace Universe.Core.Contracts.User;

public record StudentResponse(
    Guid Id,
    string Name,
    string StudentCode,
    string NationalIdOrPassport,
    Gender? Gender
);