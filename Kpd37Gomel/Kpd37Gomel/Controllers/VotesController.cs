using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DataAccess.Models;
using Kpd37Gomel.DTO;
using Microsoft.AspNetCore.Authorization;
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
            var responseData = voteList.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreateDateUtc)
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
            if (vote == null || vote.IsDeleted)
            {
                return this.NotFound("Голосование не найдено.");
            }

            VoteDetailsDTO responseData = new VoteDetailsDTO();
            responseData.Vote = this._mapper.Map<VoteDTO>(vote);
            responseData.Vote.Variants = responseData.Vote.Variants.OrderBy(x => x.SequenceIndex).ToList();
            if (vote.Choices.Any(x => x.ApartmentId == apartmentId))
            {
                var apartmentVoteChoise = vote.Choices.First(x => x.ApartmentId == apartmentId);
                responseData.IsPassed = true;
                var voteResult = vote.Variants.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => (double) 0);
                foreach (var voteChoice in vote.Choices)
                {
                    voteResult[voteChoice.VoteVariant.Id] +=
                        vote.UseVoteRate ? voteChoice.VoteRate.GetValueOrDefault() : 1;
                }

                responseData.Result = new VoteResultTinyDTO
                {
                    Voices = voteResult,
                    VoteChoise = apartmentVoteChoise.VoteVariantId
                };
            }

            return this.Ok(responseData);
        }

        [HttpPost]
        [Authorize(Policy = "OnlyApiAdmin")]
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
            responseData.Variants = responseData.Variants.OrderBy(x => x.SequenceIndex).ToList();

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

            await this._voteService.CreateVoteChoiseAsync(voteId, voteVariant.Id, apartmentId,
                vote.UseVoteRate ? apartment.VoteRate : (double?) null);

            vote = await this._voteService.GetVoteByIdAsync(voteId);

            VoteDetailsDTO responseData = new VoteDetailsDTO();
            responseData.Vote = this._mapper.Map<VoteDTO>(vote);
            responseData.Vote.Variants = responseData.Vote.Variants.OrderBy(x => x.SequenceIndex).ToList();
            if (vote.Choices.Any(x => x.ApartmentId == apartmentId))
            {
                responseData.IsPassed = true;
                var voteResult = vote.Variants.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => (double) 0);
                foreach (var voteChoice in vote.Choices)
                {
                    voteResult[voteChoice.VoteVariant.Id] +=
                        vote.UseVoteRate ? voteChoice.VoteRate.GetValueOrDefault() : 1;
                }

                responseData.Result = new VoteResultTinyDTO { Voices = voteResult };
            }

            return this.Ok(responseData);
        }

        [HttpGet("{voteId}/details")]
        [Authorize(Policy = "OnlyApiAdmin")]
        public async Task<IActionResult> GetDetailedVotingListAsync([FromRoute] Guid voteId)
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
                return this.NotFound("Голосование не найдено.");
            }

            var apartments = await this._apartmentService.GetApartmentsAsync();

            List<ApartmentVoteResultDTO> responseData = new List<ApartmentVoteResultDTO>();
            foreach (var apartment in apartments.OrderBy(x => x.ApartmentNumber))
            {
                var apartmentVoteResult = new ApartmentVoteResultDTO();
                apartmentVoteResult.ApartmentNumber = apartment.ApartmentNumber;
                apartmentVoteResult.LivingSpace = apartment.TotalArea; // TODO: FIX ASAP

                var voteChoise = vote.Choices.FirstOrDefault(x => x.ApartmentId == apartment.Id);
                if (voteChoise == null)
                {
                    apartmentVoteResult.VoteRate = apartment.VoteRate;
                    apartmentVoteResult.VoteChoise = "Не проголосовали";
                }
                else
                {
                    apartmentVoteResult.VoteRate = voteChoise.VoteRate.GetValueOrDefault();
                    apartmentVoteResult.VoteChoise = voteChoise.VoteVariant.Text;
                }

                responseData.Add(apartmentVoteResult);
            }

            return this.Ok(responseData);
        }

        [HttpPut("{voteId}")]
        [Authorize(Policy = "OnlyApiAdmin")]
        public async Task<IActionResult> UpdateVoteAsync([FromRoute] Guid voteId, [FromBody] VoteDTO vote)
        {
            try
            {
                var currentUser = this.HttpContext.User;

                var tenantIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "tenant_id");
                Guid tenantId;
                if (tenantIdClaim == null || !Guid.TryParse(tenantIdClaim.Value, out tenantId))
                {
                    throw new Exception("Неизвестный пользователь.");
                }

                var voteToUpdate = new Vote();
                voteToUpdate.Title = vote.Title;
                voteToUpdate.Description = vote.Description;

                voteToUpdate = await this._voteService.UpdateVoteAsync(voteId, voteToUpdate);
                var responseData = this._mapper.Map<VoteDTO>(voteToUpdate);
                responseData.Variants = responseData.Variants.OrderBy(x => x.SequenceIndex).ToList();

                return this.Ok(responseData);
            }
            catch (Exception e)
            {
                return this.BadRequest(new {message = e.Message});
            }
        }

        [HttpDelete("{voteId}")]
        [Authorize(Policy = "OnlyApiAdmin")]
        public async Task<IActionResult> DeleteVoteAsync([FromRoute] Guid voteId)
        {
            try
            {
                var currentUser = this.HttpContext.User;

                var tenantIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "tenant_id");
                Guid tenantId;
                if (tenantIdClaim == null || !Guid.TryParse(tenantIdClaim.Value, out tenantId))
                {
                    throw new Exception("Неизвестный пользователь.");
                }

                await this._voteService.DeleteVoteAsync(voteId);

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(new { message = e.Message });
            }
        }
    }
}