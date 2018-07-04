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
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kpd37Gomel.Controllers
{
    [Route("api/v1/apartments")]
    [ApiController]
    [Authorize]
    public class ApartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IApartmentService _apartmentService;
        private readonly ITenantService _tenantService;

        public ApartmentController(IMapper mapper, IApartmentService apartmentService, ITenantService tenantService)
        {
            this._mapper = mapper;
            this._apartmentService = apartmentService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetApartmentsAsync()
        {
            var apartments = await this._apartmentService.GetApartmentsAsync();
            var responseData = apartments
                .OrderBy(x => x.ApartmentNumber)
                .Select(x => this._mapper.Map<ApartmentDTO>(x))
                .ToList();

            return this.Ok(responseData);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApartmentAsync([FromForm] string values)
        {
            var apartmentDto = new ApartmentDTO();
            JsonConvert.PopulateObject(values, apartmentDto);

            var apartmentToCreate = this._mapper.Map<Apartment>(apartmentDto);
            apartmentToCreate.Id = Guid.NewGuid();

            var createdApartment = await this._apartmentService.CreateApartmentAsync(apartmentToCreate);
            var responseData = this._mapper.Map<ApartmentDTO>(createdApartment);

            return this.Ok(responseData);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateApartmentAsync([FromForm] Guid key, [FromForm] string values)
        {
            var existingApartment = await this._apartmentService.GetApartmentByIdAsync(key);
            if (existingApartment == null)
            {
                return this.NotFound(key);
            }

            JsonConvert.PopulateObject(values, existingApartment);
            var updatedApartment = await this._apartmentService.UpdateApartmentAsync(key, existingApartment);
            var responseData = this._mapper.Map<ApartmentDTO>(updatedApartment);

            return this.Ok(responseData);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApartmentAsync([FromForm] Guid key)
        {
            var existingApartment = await this._apartmentService.GetApartmentByIdAsync(key);
            if (existingApartment == null)
            {
                return this.NotFound(key);
            }

            await this._apartmentService.DeleteApartmentAsync(key);
            return this.Ok();
        }

        [HttpGet("{apartmentId}/tenants")]
        public async Task<IActionResult> GetApartmentTenantsAsync([FromRoute] Guid apartmentId)
        {
            var tenants = await this._tenantService.GetApartmentTenantsAsync(apartmentId);
            var responseData = tenants.Select(x => this._mapper.Map<TenantDTO>(x)).ToList();
            foreach (var tenant in responseData)
            {
                var temp = tenants.FirstOrDefault(x => x.Id == tenant.Id);
                if (temp != null)
                {
                    tenant.IsOwner = temp.ApartmentTenants.Any(x => x.ApartmentId == apartmentId && x.IsOwner);
                }
            }

            return this.Ok(responseData);
        }

        [HttpPost("{apartmentId}/tenants")]
        public async Task<IActionResult> CreateApartmentTenantsAsync([FromRoute] Guid apartmentId,
            [FromForm] string values)
        {
            var tenantDto = new TenantDTO();
            JsonConvert.PopulateObject(values, tenantDto);

            TenantDTO responseData;
            var tenants = await this._tenantService.GetTenantsAsync();
            var existingTenant = tenants.FirstOrDefault(x => x.FirstName == tenantDto.FirstName &&
                                                             x.MiddleName == tenantDto.MiddleName &&
                                                             x.LastName == tenantDto.LastName &&
                                                             x.ApartmentTenants.Any(apt =>
                                                                 apt.ApartmentId == apartmentId));
            if (existingTenant == null)
            {
                var tenantToCreate = this._mapper.Map<Tenant>(tenantDto);
                tenantToCreate.Id = Guid.NewGuid();

                var createdTenant =
                    await this._tenantService.CreateTenantAsync(tenantToCreate, apartmentId, tenantDto.IsOwner);
                responseData = this._mapper.Map<TenantDTO>(createdTenant);
                responseData.IsOwner = tenantDto.IsOwner;
            }
            else
            {
                var apartmentTenant =
                    await this._tenantService.CreateApartmentTenantAsync(existingTenant.Id, apartmentId,
                        tenantDto.IsOwner);
                responseData = this._mapper.Map<TenantDTO>(existingTenant);
                responseData.IsOwner = tenantDto.IsOwner;
            }

            return this.Ok(responseData);
        }

        [HttpPut("{apartmentId}/tenants")]
        public async Task<IActionResult> UpdateTenantAsync([FromRoute] Guid apartmentId, [FromForm] Guid key,
            [FromForm] string values)
        {
            var existingApartment = await this._apartmentService.GetApartmentByIdAsync(apartmentId);
            if (existingApartment == null)
            {
                return this.NotFound(apartmentId);
            }

            var existingTenant = await this._tenantService.GetTenantByIdAsync(key);
            if (existingTenant == null)
            {
                return this.NotFound(key);
            }

            var tenantDto = new TenantDTO();
            JsonConvert.PopulateObject(values, tenantDto);
            JsonConvert.PopulateObject(values, existingTenant);
            var isOwner = values.Contains("isOwner")
                ? tenantDto.IsOwner
                : existingTenant.ApartmentTenants.FirstOrDefault(x => x.ApartmentId == apartmentId && x.TenantId == key)
                    ?.IsOwner;
            var tenant = await this._tenantService.UpdateTenantAsync(key, apartmentId, isOwner ?? true, existingTenant);
            var responseData = this._mapper.Map<TenantDTO>(tenant);

            return this.Ok(responseData);
        }

        [HttpDelete("{apartmentId}/tenants")]
        public async Task<IActionResult> DeleteApartmentTenantAsync([FromRoute] Guid apartmentId, [FromForm] Guid key)
        {
            await this._tenantService.DeleteApartmentTenantAsync(key, apartmentId);

            return this.Ok();
        }
    }
}
