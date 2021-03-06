﻿using System;
using System.Collections.Generic;

namespace Kpd37Gomel.DataAccess.Models
{
    public class Vote
    {
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }
        public Tenant Author { get; set; }

        public DateTime CreateDateUtc { get; set; }
        public bool IsPassed { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool UseVoteRate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDateUtc { get; set; }

        public virtual ICollection<VoteVariant> Variants { get; set; } = new List<VoteVariant>();
    }
}