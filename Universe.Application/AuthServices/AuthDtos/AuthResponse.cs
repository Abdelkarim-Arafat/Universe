using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AuthServices.AuthDtos;

public record AuthResponse(
    string Id,
    string Name,
    string? Email,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration
);