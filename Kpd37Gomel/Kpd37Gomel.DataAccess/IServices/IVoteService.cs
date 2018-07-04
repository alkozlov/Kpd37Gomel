using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;

namespace Kpd37Gomel.DataAccess.IServices
{
    public interface IVoteService
    {
        Task<Vote> CreateVoteAsync(Vote vote);

        Task<IEnumerable<Vote>> GetVotesAsync();

        Task<Vote> GetVoteByIdAsync(Guid voteId);

        Task AcceptVoteChoiseAsync(Guid voteId, Guid voteVariantId, Guid apartmentId, double? voteRate = null);
    }
}