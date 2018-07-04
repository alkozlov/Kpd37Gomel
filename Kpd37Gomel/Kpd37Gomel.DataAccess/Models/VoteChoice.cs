using System;

namespace Kpd37Gomel.DataAccess.Models
{
    public class VoteChoice
    {
        public Guid Id { get; set; }

        public Guid VoteId { get; set; }
        public Vote Vote { get; set; }

        public Guid VoteVariantId { get; set; }
        public VoteVariant VoteVariant { get; set; }

        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        public double? VoteRate { get; set; }

        public DateTime VoteDateUtc { get; set; }
    }
}