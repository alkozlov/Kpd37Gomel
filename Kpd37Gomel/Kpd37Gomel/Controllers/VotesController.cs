using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DataAccess.Models;
using Kpd37Gomel.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    [Route("api/v1/votes")]
    [ApiController]
    [Authorize]
    public class VotesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVoteService _voteService;
        private readonly IApartmentService _apartmentService;

        public VotesController(IMapper mapper, IVoteService voteService, IApartmentService apartmentService)
        {
            this._mapper = mapper;
            this._voteService = voteService;
            this._apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVoteListAsync()
        {
            var voteList = await this._voteService.GetVotesAsync();
            var responseData = voteList.OrderByDescending(x => x.CreateDateUtc)
                .Select(x => this._mapper.Map<VoteDTO>(x)).ToList();

            return this.Ok(responseData);
        }

        [HttpGet("{voteId}")]
        public async Task<IActionResult> GetVoteDetailsAsync([FromRoute] Guid voteId)
        {
            var currentUser = this.HttpContext.User;
            var tenantIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "tenant_id");
            var apartmentIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "apartment_id");
            Guid tenantId, apartmentId;
            if (tenantIdClaim == null || !Guid.TryParse(tenantIdClaim.Value, out tenantId) ||
                apartmentIdClaim == null || !Guid.TryParse(apartmentIdClaim.Value, out apartmentId))
            {
                throw new Exception("Неизвестный пользователь.");
            }

            var vote = await this._voteService.GetVoteByIdAsync(voteId);
            if (vote == null)
            {
                return this.NotFound("Неверный код голосования.");
            }

            VoteDetailsDTO responseData = new VoteDetailsDTO();
            responseData.Vote = this._mapper.Map<VoteDTO>(vote);
            if (vote.Choices.Any(x => x.ApartmentId == apartmentId))
            {
                responseData.IsPassed = true;
                var voteResult = vote.Variants.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => (double) 0);
                foreach (var voteChoice in vote.Choices)
                {
                    voteResult[voteChoice.VoteVariant.Id] +=
                        vote.UseVoteRate ? voteChoice.VoteRate.GetValueOrDefault() : 1;
                }

                responseData.Result = new VoteResultTinyDTO {Voices = voteResult};
            }

            return this.Ok(responseData);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVoteAsync([FromBody] VoteDTO vote)
        {
            var currentUser = this.HttpContext.User;

            var tenantIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "tenant_id");
            Guid tenantId;
            if (tenantIdClaim == null || !Guid.TryParse(tenantIdClaim.Value, out tenantId))
            {
                throw new Exception("Неизвестный пользователь.");
            }

            Vote voteToCreate = new Vote();
            voteToCreate.Id = Guid.NewGuid();
            voteToCreate.AuthorId = tenantId;
            voteToCreate.CreateDateUtc = DateTime.UtcNow;
            voteToCreate.Title = vote.Title;
            voteToCreate.Description = vote.Description;
            voteToCreate.UseVoteRate = vote.UseVoteRate;
            voteToCreate.Variants = vote.Variants
                .Select(x => new VoteVariant
                {
                    Id = Guid.NewGuid(),
                    Text = x.Text,
                    VoteId = voteToCreate.Id
                }).ToList();

            var createdVote = await this._voteService.CreateVoteAsync(voteToCreate);
            var responseData = this._mapper.Map<VoteDTO>(createdVote);

            return this.Ok(responseData);
        }

        [HttpPost("{voteId}/send-vote")]
        public async Task<IActionResult> AcceptVoteChoiseAsync([FromRoute] Guid voteId, [FromBody] VoteVariantDTO voteVariant)
        {
            if (voteVariant == null || voteVariant.Id == Guid.Empty)
            {
                return this.BadRequest();
            }

            var currentUser = this.HttpContext.User;
            var tenantIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "tenant_id");
            var apartmentIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "apartment_id");
            Guid tenantId, apartmentId;
            if (tenantIdClaim == null || !Guid.TryParse(tenantIdClaim.Value, out tenantId) ||
                apartmentIdClaim == null || !Guid.TryParse(apartmentIdClaim.Value, out apartmentId))
            {
                throw new Exception("Неизвестный пользователь.");
            }

            var vote = await this._voteService.GetVoteByIdAsync(voteId);
            if (vote == null)
            {
                return this.NotFound("Неверный код голосования.");
            }

            if (vote.Choices.Any(x => x.ApartmentId == apartmentId))
            {
                throw new Exception("Вы уже приняли участие в этом голосовании.");
            }

            var apartment = await this._apartmentService.GetApartmentByIdAsync(apartmentId);

            await this._voteService.AcceptVoteChoiseAsync(voteId, voteVariant.Id, apartmentId,
                vote.UseVoteRate ? apartment.VoteRate : (double?) null);

            vote = await this._voteService.GetVoteByIdAsync(voteId);

            VoteDetailsDTO responseData = new VoteDetailsDTO();
            responseData.Vote = this._mapper.Map<VoteDTO>(vote);
            if (vote.Choices.Any(x => x.ApartmentId == apartmentId))
            {
                responseData.IsPassed = true;
                var voteResult = vote.Variants.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => (double)0);
                foreach (var voteChoice in vote.Choices)
                {
                    voteResult[voteChoice.VoteVariant.Id] +=
                        vote.UseVoteRate ? voteChoice.VoteRate.GetValueOrDefault() : 1;
                }

                responseData.Result = new VoteResultTinyDTO { Voices = voteResult };
            }

            return this.Ok(responseData);
        }
    }
}