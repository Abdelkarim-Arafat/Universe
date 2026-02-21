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

    public async Task<Student?> GetStudentByIdAsync(Guid UserId, CancellationToken cancellationToken)
        => await _context.Students.SingleOrDefaultAsync(x => x.UserId == UserId && !x.IsDeleted, cancellationToken);

    //public async Task UpdatePersonalDataAsync(Student student, CancellationToken cancellationToken)
    //{
    //    _context.Students.ExecuteUpdateAsync();
    //    await _context.SaveChangesAsync(cancellationToken);
    //}

    public async Task UpdatePersonalDataAsync(Guid StudentId, CancellationToken cancellationToken)
        => await _context.Students
        .Where(x => x.Id == StudentId)
        .ExecuteUpdateAsync(setter =>
            setter.SetProperty(x => x.ArabicName, x => x.ArabicName)
        );

    public async Task<bool> IsStudentCodeExistsAsync(Guid CollegeId , Guid? UserId , string studentCode , CancellationToken cancellationToken)
        => await _context.Students
        .AnyAsync(x => x.CollegeId == CollegeId &&
        !x.IsDeleted &&
        (UserId == null || x.UserId == UserId), cancellationToken);

    public async Task<bool> IsStudentNationalIdExistsAsync(Guid CollegeId , Guid? UserId , string NationalId, CancellationToken cancellationToken)
        => await _context.Students
        .AnyAsync(x => x.CollegeId == CollegeId &&
        !x.IsDeleted &&
        (UserId == null || x.UserId == UserId) , cancellationToken);
}
