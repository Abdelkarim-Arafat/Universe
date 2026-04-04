namespace Universe.Application.UserServices.UserDtos;

public record StudentInfoResponse
(
string StudentName,
string LevelName,
string StudentCode,
int RegisteredHours,
int MaxAllowedHours,
int MinAllowedHours,
decimal GPA
);
