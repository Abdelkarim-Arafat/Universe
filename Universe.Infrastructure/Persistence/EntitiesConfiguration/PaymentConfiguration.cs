using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Student)
            .WithMany(s => s.Payments)
            .HasForeignKey(p => p.StudentId);

        builder.HasOne(p => p.Service)
            .WithMany(s => s.Payments)
            .HasForeignKey(p => p.ServiceId);
    }
}