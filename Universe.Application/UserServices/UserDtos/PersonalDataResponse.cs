using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.UserServices.UserDtos;

public record PersonalDataResponse(
    string Name,
    string StudentCode,
    string NationalIdOrPassport,
    Religion Religion,
    Gender Gender,
    DateOnly? DateOfBirth,
    DateOnly? PlaceOfBirth,
    DateOnly? Nationality
);