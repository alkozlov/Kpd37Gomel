using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kpd37Gomel.DataAccess.Configurations
{
    class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("Vote");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.AuthorId)
                .IsRequired()
                .HasColumnName("AuthorId");

            builder.Property(p => p.CreateDateUtc)
                .IsRequired()
                .HasColumnName("CreateDateUtc");

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Title");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("Description");

            builder.Property(p => p.UseVoteRate)
                .IsRequired()
                .HasColumnName("UseVoteRate");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("IsDeleted");

            builder.HasMany(p => p.Variants).WithOne(p => p.Vote).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Choices).WithOne(p => p.Vote).OnDelete(DeleteBehavior.Cascade);
        }
    }
}