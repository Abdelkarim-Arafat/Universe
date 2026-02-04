using Universe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Interfaces;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
    string? ValidateToken(string token);
}