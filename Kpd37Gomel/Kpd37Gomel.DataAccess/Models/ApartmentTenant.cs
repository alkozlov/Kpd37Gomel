using System;

namespace Kpd37Gomel.DataAccess.Models
{
    public class ApartmentTenant
    {
        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public bool IsOwner { get; set; }
    }
}