using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.UserDtos;

public record FamilyDataResponse(
    string GuardianName,
    string RelationshipDegree,
    string Job,
    string MotherName,
    string City,
    string Email,
    string PhoneNumber,
    string Address
);