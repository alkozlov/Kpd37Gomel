﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IQueryable<Vote>> GetVotesAsync(bool includeDeleted = false)
        {
            var votes = await this.Context.Votes
                .Include(i => i.Author)
                .Include(i => i.Variants).ThenInclude(i => i.ApartmentVoteChoices).ThenInclude(i => i.Apartment)
                .Where(x => x.IsDeleted == includeDeleted)
                .ToListAsync();

            return votes.AsQueryable();
        }

        public async Task<Vote> GetVoteByIdAsync(Guid voteId)
        {
            var vote = await this.Context.Votes
                .AsTracking()
                .Include(i => i.Variants)
                .ThenInclude(i => i.ApartmentVoteChoices)
                .ThenInclude(i => i.Apartment)
                .Include(i => i.Author)
                .FirstOrDefaultAsync(x => x.Id == voteId);

            return vote;
        }

        public async Task<ApartmentVoteChoice> CreateVoteChoiseAsync(Guid voteVariantId, Guid apartmentId,
            double? voteRate = null)
        {
            ApartmentVoteChoice voteChoice = new ApartmentVoteChoice();
            voteChoice.Id = Guid.NewGuid();
            voteChoice.VoteVariantId = voteVariantId;
            voteChoice.ApartmentId = apartmentId;
            voteChoice.ParticipationDateUtc = DateTime.UtcNow;
            voteChoice.VoteRate = voteRate;

            this.Context.ApartmentVoteChoices.Add(voteChoice);
            await this.Context.SaveChangesAsync();

            return voteChoice;
        }

        public async Task<Vote> UpdateVoteAsync(Guid voteId, Vote vote)
        {
            var existingVote = await this.Context.Votes
                .FirstOrDefaultAsync(x => x.Id == voteId);

            if (existingVote == null || existingVote.IsDeleted)
            {
                throw new Exception("Голосование не найдено.");
            }

            existingVote.Title = vote.Title;
            existingVote.Description = vote.Description;
            await this.Context.SaveChangesAsync();

            return existingVote;
        }

        public async Task DeleteVoteAsync(Guid voteId)
        {
            var existingVote = await this.Context.Votes
                .FirstOrDefaultAsync(x => x.Id == voteId);

            if (existingVote == null || existingVote.IsDeleted)
            {
                throw new Exception("Голосование не найдено.");
            }

            existingVote.IsDeleted = true;
            existingVote.DeletionDateUtc = DateTime.UtcNow;
            await this.Context.SaveChangesAsync();
        }
    }
}