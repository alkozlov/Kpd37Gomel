using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;

namespace Kpd37Gomel.DataAccess.IServices
{
    public interface ITenantService
    {
        Task<IEnumerable<Tenant>> GetApartmentTenantsAsync(Guid apartmentId);

        Task<IQueryable<Tenant>> GetTenantsAsync(bool inclideDeleted = false);

        Task<Tenant> GetTenantByIdAsync(Guid tenantId);

        Task<Tenant> CreateTenantAsync(Tenant tenant, Guid apartmentId, bool isOwner);

        Task<Tenant> CreateTenantAsync(Tenant tenant);

        Task<Tenant> UpdateTenantAsync(Guid tenantId, Guid apartmentId, bool isOwner, Tenant modifiedTenant);

        Task<Tenant> UpdateTenantAsync(Tenant modifiedTenant);

        Task DeleteTenantAsync(Guid tenantId);
    }
}