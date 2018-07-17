using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Kpd37Gomel.DataAccess.Configurations
{
    class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenant");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.ApartmentId)
                .IsRequired()
                .HasDefaultValue(Guid.Empty)
                .HasColumnName("ApartmentId");

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("FirstName");

            builder.Property(p => p.MiddleName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("MiddleName");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("LastName");

            builder.Property(p => p.IsAdmin)
                .IsRequired()
                .HasColumnName("IsAdmin");

            builder.Property(p => p.IsOwner)
                .IsRequired()
                .HasDefaultValue(true)
                .HasColumnName("IsOwner");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("IsDeleted");

            builder.Property(p => p.DeletionDateUtc)
                .HasColumnName("DeletionDateUtc");
        }
    }
}