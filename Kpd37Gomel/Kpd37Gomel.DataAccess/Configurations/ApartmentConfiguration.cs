using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kpd37Gomel.DataAccess.Configurations
{
    class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.ToTable("Apartment");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.ApartmentNumber)
                .IsRequired()
                .HasColumnName("ApartmentNumber");

            builder.Property(p => p.FloorNumber)
                .HasColumnName("FloorNumber");

            builder.Property(p => p.TotalAreaSnb)
                .HasColumnName("TotalAreaSnb");

            builder.Property(p => p.TotalArea)
                .IsRequired()
                .HasColumnName("TotalArea");

            builder.Property(p => p.LivingSpace)
                .HasColumnName("LivingSpace");

            builder.Property(p => p.VoteRate)
                .IsRequired()
                .HasColumnName("VoteRate");

            builder.Property(p => p.RoomsCount)
                .HasColumnName("RoomsCount");
        }
    }
}