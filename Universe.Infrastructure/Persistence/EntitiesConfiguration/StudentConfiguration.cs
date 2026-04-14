using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

internal class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);


        builder.HasOne(s => s.Advisor)
            .WithMany(u => u.AdvisedStudents)
            .HasForeignKey(s => s.AdvisorId);

        builder.Property(s => s.Id).ValueGeneratedNever();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.ImageUrl)
            .HasMaxLength(500);

        builder.Property(s => s.StudentCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.PlaceOfBirth)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Nationality)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.NationalIdOrPassport)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.MaritalStatus)
            .HasConversion(
                to => to.HasValue ? to.Value.ToString() : null,
                from => from == null ? null : Enum.Parse<MaritalStatus>(from)
            )
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(s => s.Gender)
            .HasConversion(
                to => to.HasValue ? to.Value.ToString() : null,
                from => from == null ? null : Enum.Parse<Gender>(from)
            )
            .HasMaxLength(50)
            .IsRequired(false);

        // ================== Contact Info ==================
        builder.OwnsOne(s => s.ContactInfo, contact =>
        {
            contact.Property(c => c.City)
                .HasMaxLength(100)
                .IsRequired();

            contact.Property(c => c.Address)
                .HasMaxLength(300)
                .IsRequired();

            contact.Property(c => c.PostalCode)
                .HasMaxLength(20);

            contact.Property(c => c.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            contact.Property(c => c.Email)
                .HasMaxLength(150)
                .IsRequired();
        });

        // ================== Parent Info ==================
        builder.OwnsOne(s => s.ParentInfo, parent =>
        {
            parent.Property(p => p.GuardianName)
                .HasMaxLength(100)
                .IsRequired();

            parent.Property(p => p.Job)
                .HasMaxLength(150)
                .IsRequired();

            parent.Property(p => p.GuardianCity)
                .HasMaxLength(100)
                .IsRequired();

            parent.Property(p => p.GuardianAddress)
                .HasMaxLength(300)
                .IsRequired();

            parent.Property(p => p.GuardianPhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            parent.Property(p => p.GuardianEmail)
                .HasMaxLength(150);

            parent.Property(p => p.RelationshipDegree)
                .HasMaxLength(50);

            parent.Property(p => p.MotherName)
                .HasMaxLength(100);

        });

        // ================== Previous Qualification ==================
        builder.OwnsOne(s => s.PreviousQualification, qualification =>
        {
            qualification.Property(q => q.SchoolName)
                .HasMaxLength(100)
                .IsRequired();

            qualification.Property(q => q.Qualification)
                .HasMaxLength(150)
                .IsRequired();

            qualification.Property(q => q.TotalGrade)
                .HasColumnType("decimal(6,2)")
                .IsRequired();

            qualification.Property(x => x.AdmissionType)
            .HasConversion(
                to => to.HasValue ? to.Value.ToString() : null,
                from => from == null ? null : Enum.Parse<AdmissionType>(from)
            )
            .HasMaxLength(50)
            .IsRequired(false);
        });

        // ================== Military Info ==================
        builder.OwnsOne(s => s.MilitaryInfo, military =>
        {

            military.Property(m => m.MilitaryNumber)
                .HasMaxLength(50);

            military.Property(m => m.DecisionNumber)
                .HasMaxLength(50);

            military.Property(x => x.MilitaryStatus)
            .HasConversion(
                to => to.HasValue ? to.Value.ToString() : null,
                from => from == null ? null : Enum.Parse<MilitaryStatus>(from)
            )
            .HasMaxLength(50)
            .IsRequired(false);
        });
    }
}
