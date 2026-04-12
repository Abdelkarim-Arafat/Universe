using Universe.Core.Enums;

namespace Universe.Application.UserServices.UserDtos;

public record StudentResponse(
    Guid Id,
    string Name,
    string StudentCode,
    string NationalIdOrPassport,
    Gender? Gender
);