using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kpd37Gomel.DataAccess.Configurations
{
    class ApartmentTenantConfiguration : IEntityTypeConfiguration<ApartmentTenant>
    {
        public void Configure(EntityTypeBuilder<ApartmentTenant> builder)
        {
            builder.ToTable("ApartmentTenant");

            builder.HasKey(p => new {p.ApartmentId, p.TenantId});

            builder.Property(p => p.ApartmentId)
                .HasColumnName("ApartmentId");

            builder.Property(p => p.TenantId)
                .HasColumnName("TenantId");

            builder.Property(p => p.IsOwner)
                .IsRequired()
                .HasColumnName("IsOwner");

            builder.HasOne(p => p.Apartment)
                .WithMany(p => p.ApartmentTenants)
                .HasForeignKey(p => p.ApartmentId);

            builder.HasOne(p => p.Tenant)
                .WithMany(p => p.ApartmentTenants)
                .HasForeignKey(p => p.TenantId);
        }
    }
}