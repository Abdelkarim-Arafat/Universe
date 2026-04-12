namespace Universe.Application.UserServices.UserDtos;

public record StudentInfoResponse
(
string StudentName,
string LevelName,
string StudentCode,
decimal RegisteredHours,
int MaxAllowedHours,
int MinAllowedHours,
decimal GPA
);
