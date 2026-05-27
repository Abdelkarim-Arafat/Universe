namespace Universe.Core.Contracts.Auth;

public record AuthResponse(
    Guid Id,
    Guid CollegeId,
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