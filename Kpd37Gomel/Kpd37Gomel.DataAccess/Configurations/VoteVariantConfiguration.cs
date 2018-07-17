using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kpd37Gomel.DataAccess.Configurations
{
    class VoteVariantConfiguration : IEntityTypeConfiguration<VoteVariant>
    {
        public void Configure(EntityTypeBuilder<VoteVariant> builder)
        {
            builder.ToTable("VoteVariant");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.VoteId)
                .IsRequired()
                .HasColumnName("VoteId");

            builder.Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Text");

            builder.Property(p => p.SequenceIndex)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnName("SequenceIndex");

            builder.HasOne(p => p.Vote)
                .WithMany(p => p.Variants)
                .HasForeignKey(p => p.VoteId);

            builder.HasMany(p => p.ApartmentVoteChoices)
                .WithOne(p => p.VoteVariant)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}