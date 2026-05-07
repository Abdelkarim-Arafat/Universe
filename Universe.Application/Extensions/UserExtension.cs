using System.Security.Claims;

namespace Universe.Application.Extensions;

public static class UserExtension
{
    public static string? GetUserId(this ClaimsPrincipal user)
        => user.FindFirstValue(ClaimTypes.NameIdentifier);
}
