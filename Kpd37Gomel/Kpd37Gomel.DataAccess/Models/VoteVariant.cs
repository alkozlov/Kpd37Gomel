using System;
using System.Collections.Generic;

namespace Kpd37Gomel.DataAccess.Models
{
    public class VoteVariant
    {
        public Guid Id { get; set; }

        public Guid VoteId { get; set; }
        public Vote Vote { get; set; }

        public string Text { get; set; }
        public int SequenceIndex { get; set; }

        public virtual ICollection<ApartmentVoteChoice> ApartmentVoteChoices { get; set; } = new List<ApartmentVoteChoice>();
    }
}