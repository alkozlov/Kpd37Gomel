using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DataAccess.Models;
using Kpd37Gomel.DTO;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;

namespace Kpd37Gomel.Controllers
{
    [Authorize]
    public class VoteController : BaseODataController
    {
        private readonly IVoteService _voteService;
        private readonly ITenantService _tenantService;
        private readonly IApartmentService _apartmentService;

        public VoteController(IVoteService voteService, ITenantService tenantService,
            IApartmentService apartmentService)
        {
            this._voteService = voteService;
            this._tenantService = tenantService;
            this._apartmentService = apartmentService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var votes = await this._voteService.GetVotesAsync();

                return this.Ok(votes.Where(x => !x.IsDeleted).AsQueryable());
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] Guid key)
        {
            try
            {
                var currentUser = this.HttpContext.User;

                var apartmentIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "apartment_id");
                if (apartmentIdClaim == null || !Guid.TryParse(apartmentIdClaim.Value, out var apartmentId))
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                var apartment = await this._apartmentService.GetApartmentByIdAsync(apartmentId);
                if (apartment == null)
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                var vote = await this._voteService.GetVoteByIdAsync(key);
                if (vote == null || vote.IsDeleted)
                {
                    return this.BadRequest("Голосование не найдено.");
                }

                vote.IsPassed = vote.Variants.SelectMany(x => x.ApartmentVoteChoices).Any(x => x.ApartmentId == apartmentId);

                return this.Ok(vote);
            }
            catch (Exception)
            {
                return this.BadRequest(new ODataError { Message = "Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору." });
            }
        }

        [HttpPost]
        [EnableQuery]
        [Authorize(Policy = "OnlyApiAdmin")]
        public async Task<IActionResult> Post([FromBody] Vote requestData)
        {
            try
            {
                var currentUser = this.HttpContext.User;

                var tenantIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "tenant_id");
                if (tenantIdClaim == null || !Guid.TryParse(tenantIdClaim.Value, out var tenantId))
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                var tenant = await this._tenantService.GetTenantByIdAsync(tenantId);
                if (tenant == null)
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                if (!tenant.IsAdmin)
                {
                    return this.BadRequest("Недостаточно прав доступа.");
                }

                requestData.Id = Guid.NewGuid();
                requestData.AuthorId = tenantId;
                requestData.CreateDateUtc = DateTime.UtcNow;
                foreach (var variant in requestData.Variants)
                {
                    variant.Id = Guid.NewGuid();
                }

                var vote = await this._voteService.CreateVoteAsync(requestData);

                return this.Created(vote);
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }

        [HttpPatch]
        [EnableQuery]
        [Authorize(Policy = "OnlyApiAdmin")]
        public async Task<IActionResult> Patch([FromODataUri] Guid key, [FromBody] Delta<Vote> delta)
        {
            try
            {
                var currentUser = this.HttpContext.User;

                var tenantIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "tenant_id");
                if (tenantIdClaim == null || !Guid.TryParse(tenantIdClaim.Value, out var tenantId))
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                var tenant = await this._tenantService.GetTenantByIdAsync(tenantId);
                if (tenant == null)
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                if (!tenant.IsAdmin)
                {
                    return this.BadRequest("Недостаточно прав доступа.");
                }

                var vote = await this._voteService.GetVoteByIdAsync(key);
                if (vote == null)
                {
                    return this.BadRequest("Голосование не найдено.");
                }

                delta.Patch(vote);
                vote = await this._voteService.UpdateVoteAsync(key, vote);

                return this.Updated(vote);
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "OnlyApiAdmin")]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            try
            {
                var currentUser = this.HttpContext.User;

                var tenantIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "tenant_id");
                if (tenantIdClaim == null || !Guid.TryParse(tenantIdClaim.Value, out var tenantId))
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                var tenant = await this._tenantService.GetTenantByIdAsync(tenantId);
                if (tenant == null)
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                if (!tenant.IsAdmin)
                {
                    return this.BadRequest("Недостаточно прав доступа.");
                }

                var vote = await this._voteService.GetVoteByIdAsync(key);
                if (vote == null)
                {
                    return this.BadRequest("Голосование не найдено.");
                }

                await this._voteService.DeleteVoteAsync(key);

                return this.NoContent();
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendVote([FromODataUri] Guid key, ODataActionParameters parameters)
        {
            try
            {
                var currentUser = this.HttpContext.User;

                var apartmentIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "apartment_id");
                if (apartmentIdClaim == null || !Guid.TryParse(apartmentIdClaim.Value, out var apartmentId))
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                var apartment = await this._apartmentService.GetApartmentByIdAsync(apartmentId);
                if (apartment == null)
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                var vote = await this._voteService.GetVoteByIdAsync(key);
                if (vote == null)
                {
                    return this.BadRequest("Голосование не найдено.");
                }

                if (parameters == null ||
                    !parameters.ContainsKey("VariantId") ||
                    !Guid.TryParse(parameters["VariantId"].ToString(), out var variantId) ||
                    vote.Variants.All(x => x.Id != variantId))
                {
                    return this.BadRequest("Выбранный ответ не найден.");
                }

                if (vote.Variants.SelectMany(x => x.ApartmentVoteChoices).Any(x => x.ApartmentId == apartmentId))
                {
                    return this.BadRequest("Вы уже приняли участие в этом голосовании.");
                }

                await this._voteService.CreateVoteChoiseAsync(variantId, apartmentId,
                    vote.UseVoteRate ? apartment.VoteRate : (double?) null);

                return this.NoContent();
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetCommonResult([FromODataUri] Guid key)
        {
            try
            {
                var currentUser = this.HttpContext.User;

                var apartmentIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "apartment_id");
                if (apartmentIdClaim == null || !Guid.TryParse(apartmentIdClaim.Value, out var apartmentId))
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                var apartment = await this._apartmentService.GetApartmentByIdAsync(apartmentId);
                if (apartment == null)
                {
                    return this.BadRequest("Неизвестный пользователь.");
                }

                var vote = await this._voteService.GetVoteByIdAsync(key);
                if (vote == null)
                {
                    return this.BadRequest("Голосование не найдено.");
                }

                if (vote.Variants.SelectMany(x => x.ApartmentVoteChoices).All(x => x.ApartmentId != apartment.Id))
                {
                    return this.BadRequest("Просмотр результатов доступен только после голосования.");
                }

                var responseData = new List<VoteChoiseTinyDTO>();
                foreach (var voteVariant in vote.Variants.OrderBy(x => x.SequenceIndex))
                {
                    var responseItem = new VoteChoiseTinyDTO();
                    responseItem.Id = voteVariant.Id;
                    responseItem.Text = voteVariant.Text;
                    responseItem.Value = vote.UseVoteRate
                        ? vote.Variants.SelectMany(x => x.ApartmentVoteChoices).Where(x => x.VoteVariantId == voteVariant.Id)
                            .Sum(x => x.VoteRate.GetValueOrDefault())
                        : vote.Variants.SelectMany(x => x.ApartmentVoteChoices).Count(x => x.VoteVariantId == voteVariant.Id);
                    responseData.Add(responseItem);
                }

                return this.Ok(responseData);
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }
    }
}