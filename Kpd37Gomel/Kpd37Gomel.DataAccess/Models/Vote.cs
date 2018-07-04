using System;
using System.Collections.Generic;

namespace Kpd37Gomel.DataAccess.Models
{
    public class Vote
    {
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }
        public Tenant Author { get; set; }

        public DateTime CreateDateUtc { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool UseVoteRate { get; set; }

        public List<VoteVariant> Variants { get; set; }
        public List<VoteChoice> Choices { get; set; }

        public Vote()
        {
            this.Variants = new List<VoteVariant>();
            this.Choices = new List<VoteChoice>();
        }
    }
}