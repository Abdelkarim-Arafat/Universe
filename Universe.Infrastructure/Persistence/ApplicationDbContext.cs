using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Data.Common;

namespace Universe.Infrastructure.Persistence;
// dotnet ef migrations add InitialCreate --project Universe.Infrastructure --startup-project Universe.Api
// dotnet ef migrations remove --project Universe.Infrastructure --startup-project Universe.Api
public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
    ): IdentityDbContext<ApplicationUser , ApplicationRole , Guid>(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<College> Colleges { get; set; }
    public DbSet<AcademicProgram> AcademicPrograms { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CoursePrerequisite> CoursePrerequisites { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<AcademicEvent> AcademicEvents { get; set; }
    public DbSet<AcademicYear> AcademicYears { get; set; }
    public DbSet<StudentAcademicProgram> StudentAcademicPrograms { get; set; }
    public DbSet<CourseOffering> CourseOfferings { get; set; }
    public DbSet<CourseOfferingAssessment> CourseOfferingAssessments { get; set; }
    public DbSet<StudyLoadByLevel> StudyLoadByLevels { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<ProgramSchedule> ProgramSchedules { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<StudyLoadRule> StudyLoadRules { get; set; }
    public DbSet<TeachingSession> TeachingSessions { get; set; }
    public DbSet<CourseOfferingSession> CourseOfferingSessions { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<TeachingSessionEnrollment> TeachingSessionEnrollments { get; set; }
    public DbSet<StudentAssessment> StudentAssessments { get; set; }
    public DbSet<ExamTerm> ExamTerms { get; set; }
    public DbSet<ExamSeat> ExamSeats { get; set; }
    public DbSet<ExamCommittee> ExamCommittees { get; set; }
    public DbSet<CourseOfferingExam> CourseOfferingExams { get; set; }
    public DbSet<CourseOfferingCommittee> CourseOfferingCommittees { get; set; }

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
