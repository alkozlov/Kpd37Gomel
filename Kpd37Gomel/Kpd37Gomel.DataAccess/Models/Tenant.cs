using System;

namespace Kpd37Gomel.DataAccess.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDateUtc { get; set; }

        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
    }
}