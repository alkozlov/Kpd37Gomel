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
                .Include(i => i.ApartmentTenants).ThenInclude(i => i.Tenant).ThenInclude(i => i.ApartmentTenants)
                .FirstOrDefaultAsync(x => x.Id == apartmentId);
            if (apartment == null)
            {
                throw new Exception("Apartment not found.");
            }

            return apartment.ApartmentTenants.Select(x => x.Tenant);
        }

        public async Task<IEnumerable<Tenant>> GetTenantsAsync()
        {
            var tenants = await this.Context.Tenants
                .AsNoTracking()
                .Include(i => i.ApartmentTenants).ThenInclude(i => i.Apartment)
                .ToListAsync();

            return tenants;
        }

        public async Task<Tenant> GetTenantByIdAsync(Guid tenantId)
        {
            var tenant = await this.Context.Tenants
                .AsNoTracking()
                .Include(i => i.ApartmentTenants)
                .FirstOrDefaultAsync(x => x.Id == tenantId);

            return tenant;
        }

        public async Task<Tenant> CreateTenantAsync(Tenant tenant, Guid apartmentId, bool isOwner)
        {
            ApartmentTenant apartmentTenant = new ApartmentTenant();
            apartmentTenant.ApartmentId = apartmentId;
            apartmentTenant.TenantId = tenant.Id;
            apartmentTenant.IsOwner = isOwner;

            this.Context.Tenants.Add(tenant);
            this.Context.ApartmentTenants.Add(apartmentTenant);
            await this.Context.SaveChangesAsync();

            return tenant;
        }

        public async Task<Tenant> CreateTenantAsync(Tenant tenant)
        {
            this.Context.Tenants.Add(tenant);
            await this.Context.SaveChangesAsync();

            return tenant;
        }

        public async Task<ApartmentTenant> CreateApartmentTenantAsync(Guid tenantId, Guid apartmentId, bool isOwner)
        {
            ApartmentTenant apartmentTenant = new ApartmentTenant();
            apartmentTenant.ApartmentId = apartmentId;
            apartmentTenant.TenantId = tenantId;
            apartmentTenant.IsOwner = isOwner;

            this.Context.ApartmentTenants.Add(apartmentTenant);
            await this.Context.SaveChangesAsync();

            return apartmentTenant;
        }

        public async Task<Tenant> UpdateTenantAsync(Guid tenantId, Guid apartmentId, bool isOwner, Tenant modifiedTenant)
        {
            var tenant = await this.Context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
            if (tenant == null) throw new Exception("Tenant not found.");

            tenant.FirstName = modifiedTenant.FirstName;
            tenant.MiddleName = modifiedTenant.MiddleName;
            tenant.LastName = modifiedTenant.LastName;

            var apartmentTenant =
                await this.Context.ApartmentTenants.FirstOrDefaultAsync(x =>
                    x.ApartmentId == apartmentId && x.TenantId == tenantId);
            if (apartmentTenant == null) throw new Exception("Tenant's apartment not found.");

            apartmentTenant.IsOwner = isOwner;

            this.Context.Tenants.Update(tenant);
            this.Context.ApartmentTenants.Update(apartmentTenant);
            await this.Context.SaveChangesAsync();

            return tenant;
        }

        public async Task<Tenant> UpdateTenantAsync(Tenant modifiedTenant)
        {
            this.Context.Tenants.Update(modifiedTenant);
            await this.Context.SaveChangesAsync();

            return modifiedTenant;
        }

        public async Task DeleteApartmentTenantAsync(Guid tenantId, Guid apartmentId)
        {
            var apartmentTenant =
                await this.Context.ApartmentTenants.FirstOrDefaultAsync(x =>
                    x.ApartmentId == apartmentId && x.TenantId == tenantId);
            if (apartmentTenant != null)
            {
                this.Context.ApartmentTenants.Remove(apartmentTenant);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}