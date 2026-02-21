using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Universe.Infrastructure.Persistence;
// dotnet ef migrations add InitialCreate --project Universe.Infrastructure --startup-project Universe.Api
// dotnet ef migrations remove --project Universe.Infrastructure --startup-project Universe.Api
public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
    ): IdentityDbContext<ApplicationUser , ApplicationRole , Guid>(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<College> Colleges { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseDepartment> CourseDepartments { get; set; }
    public DbSet<CoursePrerequisite> CoursePrerequisites { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }

    public DbSet<Grade> Grades { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }
}
