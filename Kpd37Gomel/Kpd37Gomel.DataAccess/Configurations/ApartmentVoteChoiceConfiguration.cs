using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kpd37Gomel.DataAccess.Configurations
{
    public class ApartmentVoteChoiceConfiguration : IEntityTypeConfiguration<ApartmentVoteChoice>
    {
        public void Configure(EntityTypeBuilder<ApartmentVoteChoice> builder)
        {
            builder.ToTable("VoteChoice");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.VoteVariantId)
                .IsRequired()
                .HasColumnName("VoteVariantId");

            builder.Property(p => p.ApartmentId)
                .IsRequired()
                .HasColumnName("ApartmentId");

            builder.Property(p => p.VoteRate)
                .HasColumnName("VoteRate");

            builder.Property(p => p.ParticipationDateUtc)
                .IsRequired()
                .HasColumnName("ParticipationDateUtc");
        }
    }
}