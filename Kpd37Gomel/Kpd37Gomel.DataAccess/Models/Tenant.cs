using System;
using System.Collections.Generic;

namespace Kpd37Gomel.DataAccess.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }

        public List<ApartmentTenant> ApartmentTenants { get; set; }

        public Tenant()
        {
            this.ApartmentTenants = new List<ApartmentTenant>();
        }
    }
}