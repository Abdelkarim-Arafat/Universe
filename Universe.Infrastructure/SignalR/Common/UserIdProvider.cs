using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Universe.Infrastructure.SignalR.Common;

internal class UserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User
            .FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
    }
}
