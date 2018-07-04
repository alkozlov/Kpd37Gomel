using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        }
    }
}