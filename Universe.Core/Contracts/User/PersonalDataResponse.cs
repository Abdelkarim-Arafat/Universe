using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Contracts.User;

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