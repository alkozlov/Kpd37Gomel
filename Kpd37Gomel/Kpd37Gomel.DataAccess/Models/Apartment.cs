using System;
using System.Collections.Generic;

namespace Kpd37Gomel.DataAccess.Models
{
    public class Apartment
    {
        public Guid Id { get; set; }
        public int ApartmentNumber { get; set; }
        public int? FloorNumber { get; set; }
        public double? TotalAreaSnb { get; set; }
        public double TotalArea { get; set; }
        public double? LivingSpace { get; set; }
        public double VoteRate { get; set; }
        public int? RoomsCount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDateUtc { get; set; }

        public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
        public virtual ICollection<ApartmentVoteChoice> ApartmentVoteChoices { get; set; } = new List<ApartmentVoteChoice>();
    }
}
