using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Kpd37Gomel.DataAccess.IServices.Implementation
{
    public class VoteService : BaseService, IVoteService
    {
        public VoteService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Vote> CreateVoteAsync(Vote vote)
        {
            this.Context.Votes.Add(vote);
            await this.Context.SaveChangesAsync();

            return vote;
        }

        public async Task<IEnumerable<Vote>> GetVotesAsync()
        {
            var votes = await this.Context.Votes.ToListAsync();

            return votes;
        }

        public async Task<Vote> GetVoteByIdAsync(Guid voteId)
        {
            var vote = await this.Context.Votes
                .AsTracking()
                .Include(i => i.Variants)
                .Include(i => i.Choices).ThenInclude(i => i.Apartment)
                .Include(i => i.Author)
                .FirstOrDefaultAsync(x => x.Id == voteId);

            return vote;
        }

        public async Task AcceptVoteChoiseAsync(Guid voteId, Guid voteVariantId, Guid apartmentId,
            double? voteRate = null)
        {
            VoteChoice voteChoice = new VoteChoice();
            voteChoice.Id = Guid.NewGuid();
            voteChoice.VoteId = voteId;
            voteChoice.VoteVariantId = voteVariantId;
            voteChoice.ApartmentId = apartmentId;
            voteChoice.VoteDateUtc = DateTime.UtcNow;
            voteChoice.VoteRate = voteRate;

            this.Context.VoteChoices.Add(voteChoice);
            await this.Context.SaveChangesAsync();
        }
    }
}