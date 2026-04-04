using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;
using Universe.Core.Enums;

namespace Universe.Application.UserServices.Commands.UpdatePersonalData;

public record UpdatePersonalDataCommand(
    [Required] Guid StudentId,
    [Required] Guid CollegeId,
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