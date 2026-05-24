namespace Universe.Core.Contracts.Student;

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