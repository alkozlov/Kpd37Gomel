using System;
using System.Collections.Generic;

namespace Kpd37Gomel.DataAccess.Models
{
    public class Apartment
    {
        public Guid Id { get; set; }
        public string ApartmentNumber { get; set; }
        public int? FloorNumber { get; set; }
        public double? TotalAreaSnb { get; set; }
        public double TotalArea { get; set; }
        public double? LivingSpace { get; set; }
        public double VoteRate { get; set; }
        public int? RoomsCount { get; set; }

        public List<ApartmentTenant> ApartmentTenants { get; set; }

        public Apartment()
        {
            this.ApartmentTenants = new List<ApartmentTenant>();
        }
    }
}
