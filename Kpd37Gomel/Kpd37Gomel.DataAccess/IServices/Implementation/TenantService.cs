using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Kpd37Gomel.DataAccess.IServices.Implementation
{
    public class TenantService : BaseService, ITenantService
    {
        public TenantService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tenant>> GetApartmentTenantsAsync(Guid apartmentId)
        {
            var apartment = await this.Context.Apartments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == apartmentId);
            if (apartment == null)
            {
                throw new Exception("Apartment not found.");
            }

            return apartment.Tenants;
        }

        public async Task<IQueryable<Tenant>> GetTenantsAsync(bool includeDeleted = false)
        {
            var tenants = await this.Context.Tenants
                .AsNoTracking()
                .Include(i => i.Apartment)
                .Where(x => x.IsDeleted == includeDeleted)
                .ToListAsync();

            return tenants.AsQueryable();
        }

        public async Task<Tenant> GetTenantByIdAsync(Guid tenantId)
        {
            var tenant = await this.Context.Tenants
                .AsNoTracking()
                //.Include(i => i.ApartmentTenants)
                .FirstOrDefaultAsync(x => x.Id == tenantId);

            return tenant;
        }

        public async Task<Tenant> CreateTenantAsync(Tenant tenant, Guid apartmentId, bool isOwner)
        {
            //ApartmentTenant apartmentTenant = new ApartmentTenant();
            //apartmentTenant.ApartmentId = apartmentId;
            //apartmentTenant.TenantId = tenant.Id;
            //apartmentTenant.IsOwner = isOwner;

            this.Context.Tenants.Add(tenant);
            //this.Context.ApartmentTenants.Add(apartmentTenant);
            await this.Context.SaveChangesAsync();

            return tenant;
        }

        public async Task<Tenant> CreateTenantAsync(Tenant tenant)
        {
            this.Context.Tenants.Add(tenant);
            await this.Context.SaveChangesAsync();

            return tenant;
        }

        public async Task<Tenant> UpdateTenantAsync(Guid tenantId, Guid apartmentId, bool isOwner, Tenant modifiedTenant)
        {
            var tenant = await this.Context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
            if (tenant == null) throw new Exception("Tenant not found.");

            tenant.FirstName = modifiedTenant.FirstName;
            tenant.MiddleName = modifiedTenant.MiddleName;
            tenant.LastName = modifiedTenant.LastName;

            //var apartmentTenant =
            //    await this.Context.ApartmentTenants.FirstOrDefaultAsync(x =>
            //        x.ApartmentId == apartmentId && x.TenantId == tenantId);
            //if (apartmentTenant == null) throw new Exception("Tenant's apartment not found.");

            //apartmentTenant.IsOwner = isOwner;

            this.Context.Tenants.Update(tenant);
            //this.Context.ApartmentTenants.Update(apartmentTenant);
            await this.Context.SaveChangesAsync();

            return tenant;
        }

        public async Task<Tenant> UpdateTenantAsync(Tenant modifiedTenant)
        {
            this.Context.Tenants.Update(modifiedTenant);
            await this.Context.SaveChangesAsync();

            return modifiedTenant;
        }

        public async Task DeleteTenantAsync(Guid tenantId)
        {
            var tenant = await this.Context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);

            if (tenant == null || tenant.IsDeleted)
            {
                throw new Exception("Жилец не найден.");
            }

            tenant.IsDeleted = true;
            tenant.DeletionDateUtc = DateTime.UtcNow;
            await this.Context.SaveChangesAsync();
        }
    }
}