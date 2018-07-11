using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;

namespace Kpd37Gomel.DataAccess.IServices
{
    public interface ITenantService
    {
        Task<IEnumerable<Tenant>> GetApartmentTenantsAsync(Guid apartmentId);

        Task<ApartmentTenant> GetApartmentTenantAsync(Guid apartmentId, Guid tenantId);

        Task<IEnumerable<Tenant>> GetTenantsAsync();

        Task<Tenant> GetTenantByIdAsync(Guid tenantId);

        Task<Tenant> CreateTenantAsync(Tenant tenant, Guid apartmentId, bool isOwner);

        Task<Tenant> CreateTenantAsync(Tenant tenant);

        Task<ApartmentTenant> CreateApartmentTenantAsync(Guid tenantId, Guid apartmentId, bool isOwner);

        Task<ApartmentTenant> CreateApartmentTenantAsync(ApartmentTenant apartmentTenant);

        Task<Tenant> UpdateTenantAsync(Guid tenantId, Guid apartmentId, bool isOwner, Tenant modifiedTenant);

        Task<Tenant> UpdateTenantAsync(Tenant modifiedTenant);

        Task<ApartmentTenant> UpdateApartmentTenantAsync(Guid apartmentId, Guid tenantId,
            ApartmentTenant modifiedApartmentTenant);

        Task DeleteApartmentTenantAsync(Guid apartmentId, Guid tenantId);
    }
}