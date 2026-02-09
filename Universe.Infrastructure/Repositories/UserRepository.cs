using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token , CancellationToken cancellationToken)
        => await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token && x.IsActive);

    //public async Task GetPasswordResetOtpAsync(Guid userId, string codeHash , CancellationToken cancellationToken)
    //    => await _context.PasswordResetOtps
    //        .SingleOrDefaultAsync(x => x.CodeHash == codeHash && x.ExpiresAt > DateTime.UtcNow);
}
