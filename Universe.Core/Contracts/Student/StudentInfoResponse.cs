namespace Universe.Core.Contracts.Student;

public record StudentInfoResponse
(
string StudentName,
string LevelName,
string StudentCode,
decimal RegisteredHours,
decimal MaxAllowedHours,
decimal MinAllowedHours,
decimal Gpa
);
