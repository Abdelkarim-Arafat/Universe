namespace Universe.Core.Contracts.User;

public record StudentInfoResponse (
    string StudentName,
    string LevelName,
    string StudentCode,
    decimal RegisteredHours,
    decimal MaxAllowedHours,
    decimal MinAllowedHours,
    decimal GPA
);
