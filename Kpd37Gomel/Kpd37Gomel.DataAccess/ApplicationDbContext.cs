using Kpd37Gomel.DataAccess.Configurations;
using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Kpd37Gomel.DataAccess
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteVariant> VoteVariants { get; set; }
        public DbSet<ApartmentVoteChoice> ApartmentVoteChoices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new ApartmentConfiguration());
            modelBuilder.ApplyConfiguration(new VoteConfiguration());
            modelBuilder.ApplyConfiguration(new VoteVariantConfiguration());
            modelBuilder.ApplyConfiguration(new ApartmentVoteChoiceConfiguration());
        }
    }
}