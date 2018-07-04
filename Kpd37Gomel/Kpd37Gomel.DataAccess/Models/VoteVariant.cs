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

        public List<VoteChoice> VoteChoices { get; set; }

        public VoteVariant()
        {
            this.VoteChoices = new List<VoteChoice>();
        }
    }
}