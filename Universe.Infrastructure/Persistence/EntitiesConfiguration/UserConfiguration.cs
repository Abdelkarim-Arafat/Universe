using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;
using Universe.Infrastructure.SeedData;
using System;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.OwnsMany(u => u.RefreshTokens).ToTable("RefreshTokens")
            .WithOwner().HasForeignKey("UserId");

        builder.OwnsMany(u => u.passwordResetOtps).ToTable("PasswordResetOtps")
            .WithOwner().HasForeignKey("UserId");

        builder.HasOne(u => u.Student)
            .WithOne(s => s.ApplicationUser)
            .HasForeignKey<Student>(s => s.UserId);

        builder.HasIndex(u => u.Name)
            .IsUnique();

        builder.HasIndex(u => u.UserName)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired(false);

        builder.Property(u => u.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.ImageUrl)
            .HasMaxLength(500);

        builder.HasData([
            new ApplicationUser
            {
                Id = DefaultUsers.SVNUId,
                Name = DefaultUsers.SVNU,
                UserName = DefaultUsers.SVNU,
                NormalizedUserName = DefaultUsers.SVNU.ToUpper(),
                Email = DefaultUsers.SVNUEmail,
                NormalizedEmail = DefaultUsers.SVNUEmail.ToUpper(),
                SecurityStamp = DefaultUsers.SVNUSecurityStamp,
                ConcurrencyStamp = DefaultUsers.SVNUConcurrencyStamp,
                EmailConfirmed = true,
                PasswordHash = DefaultUsers.SVNUPassword,
                CollegeId = DefaultCollege.Id
            },
            new ApplicationUser{
                Id = DefaultUsers.AcademicAdvisingId,
                Name = DefaultUsers.AcademicAdvising,
                UserName = DefaultUsers.AcademicAdvising,
                NormalizedUserName = DefaultUsers.AcademicAdvising.ToUpper(),
                Email = DefaultUsers.AcademicAdvisingEmail,
                NormalizedEmail = DefaultUsers.AcademicAdvisingEmail.ToUpper(),
                SecurityStamp = DefaultUsers.AcademicAdvisingSecurityStamp,
                ConcurrencyStamp = DefaultUsers.AcademicAdvisingConcurrencyStamp,
                EmailConfirmed = true,
                PasswordHash = DefaultUsers.AcademicAdvisingPassword,
                CollegeId = DefaultCollege.Id
            }]);
    }
}
