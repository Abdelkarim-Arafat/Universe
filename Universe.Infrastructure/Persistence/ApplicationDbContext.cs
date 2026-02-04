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
    public DbSet<College> Colleges { get; set; }
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
