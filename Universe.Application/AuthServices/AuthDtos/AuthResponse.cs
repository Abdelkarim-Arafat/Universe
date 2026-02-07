using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AuthServices.AuthDtos;

public record AuthResponse(
    string Id,
    string Name,
    string? Email,
    IEnumerable<string> Roles,
    IEnumerable<string> Permissions,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration
);