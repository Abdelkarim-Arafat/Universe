using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.User;

public record ParentDataResponse(
    string GuardianName,
    string RelationshipDegree,
    string Job,
    string MotherName,
    string GuardianCity,
    string GuardianEmail,
    string GuardianPhoneNumber,
    string GuardianAddress
);