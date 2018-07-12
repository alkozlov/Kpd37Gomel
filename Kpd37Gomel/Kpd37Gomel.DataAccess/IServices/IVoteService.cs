using System;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;

namespace Kpd37Gomel.DataAccess.IServices
{
    public interface IVoteService
    {
        Task<Vote> CreateVoteAsync(Vote vote);

        Task<IQueryable<Vote>> GetVotesAsync();

        Task<Vote> GetVoteByIdAsync(Guid voteId);

        Task AcceptVoteChoiseAsync(Guid voteId, Guid voteVariantId, Guid apartmentId, double? voteRate = null);

        Task<Vote> UpdateVoteAsync(Guid voteId, Vote vote);

        Task DeleteVoteAsync(Guid voteId);
    }
}