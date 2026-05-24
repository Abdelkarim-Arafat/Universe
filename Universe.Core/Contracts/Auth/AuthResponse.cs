namespace Universe.Core.Contracts.Auth;

public record AuthResponse(
    string Id,
    string CollegeId,
    string Name,
    string? ImageUrl,
    string? Email,
    IEnumerable<string> Roles,
    IEnumerable<string> Permissions,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration
);