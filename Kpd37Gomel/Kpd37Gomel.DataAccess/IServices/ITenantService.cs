using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;

namespace Kpd37Gomel.DataAccess.IServices
{
    public interface ITenantService
    {
        Task<IEnumerable<Tenant>> GetApartmentTenantsAsync(Guid apartmentId);

        Task<IEnumerable<Tenant>> GetTenantsAsync();

        Task<Tenant> GetTenantByIdAsync(Guid tenantId);

        Task<Tenant> CreateTenantAsync(Tenant tenant, Guid apartmentId, bool isOwner);

        Task<ApartmentTenant> CreateApartmentTenantAsync(Guid tenantId, Guid apartmentId, bool isOwner);

        Task<Tenant> UpdateTenantAsync(Guid tenantId, Guid apartmentId, bool isOwner, Tenant modifiedTenant);

        Task DeleteApartmentTenantAsync(Guid tenantId, Guid apartmentId);
    }
}