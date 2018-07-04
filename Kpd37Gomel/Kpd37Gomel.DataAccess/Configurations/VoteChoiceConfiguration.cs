using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kpd37Gomel.DataAccess.Configurations
{
    class VoteChoiceConfiguration : IEntityTypeConfiguration<VoteChoice>
    {
        public void Configure(EntityTypeBuilder<VoteChoice> builder)
        {
            builder.ToTable("VoteChoice");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.VoteId)
                .IsRequired()
                .HasColumnName("VoteId");

            builder.Property(p => p.VoteVariantId)
                .IsRequired()
                .HasColumnName("VoteVariantId");

            builder.Property(p => p.ApartmentId)
                .IsRequired()
                .HasColumnName("ApartmentId");

            builder.Property(p => p.VoteRate)
                .HasColumnName("VoteRate");

            builder.Property(p => p.VoteDateUtc)
                .IsRequired()
                .HasColumnName("VoteDateUtc");

            builder.HasOne(p => p.Vote)
                .WithMany(p => p.Choices)
                .HasForeignKey(p => p.VoteId);
        }
    }
}