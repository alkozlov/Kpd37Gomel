using System;

namespace Kpd37Gomel.DTO
{
    public class VoteChoiceDTO
    {
        public Guid Id { get; set; }

        public Guid VoteId { get; set; }

        public Guid VoteVariantId { get; set; }

        public Guid ApartmentId { get; set; }

        public double? VoteRate { get; set; }

        public DateTime VoteDateUtc { get; set; }
    }
}