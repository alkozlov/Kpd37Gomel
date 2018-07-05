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

namespace Kpd37Gomel.Controllers
{
    [Route("api/v1/tenants")]
    [ApiController]
    [Authorize(Policy = "OnlyApiAdmin")]
    public class TenantsController : Controller
    {
        private readonly IMapper _mapper;
        public readonly IApartmentService _apartmentService;
        private readonly ITenantService _tenantService;

        public TenantsController(IMapper mapper, IApartmentService apartmentService, ITenantService tenantService)
        {
            this._mapper = mapper;
            this._apartmentService = apartmentService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTenantsAsync()
        {
            var apartments = await this._apartmentService.GetApartmentsAsync();
            List<ApartmentTenantDTO> responseData = new List<ApartmentTenantDTO>();
            foreach (var apartment in apartments.OrderBy(x => x.ApartmentNumber))
            {
                foreach (var apartmentTenant in apartment.ApartmentTenants)
                {
                    var responseItem = new ApartmentTenantDTO();
                    responseItem.TenantId = apartmentTenant.TenantId;
                    responseItem.FirstName = apartmentTenant.Tenant.FirstName;
                    responseItem.MiddleName = apartmentTenant.Tenant.MiddleName;
                    responseItem.LastName = apartmentTenant.Tenant.LastName;
                    responseItem.ApartmentId = apartmentTenant.ApartmentId;
                    responseItem.IsOwner = apartmentTenant.IsOwner;

                    responseData.Add(responseItem);
                }
            }

            return this.Ok(responseData);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApartmentTenantAsync([FromForm] string values)
        {
            var requestData = new ApartmentTenantDTO();
            JsonConvert.PopulateObject(values, requestData);

            if (String.IsNullOrEmpty(requestData.FirstName) ||
                String.IsNullOrEmpty(requestData.MiddleName) ||
                String.IsNullOrEmpty(requestData.LastName))
            {
                throw new Exception("Фамилия, имя и отчество - обязательные данные.");
            }

            var apartment = await this._apartmentService.GetApartmentByIdAsync(requestData.ApartmentId);
            if (apartment == null)
            {
                throw new Exception("Квартира не выбрана или указан неверный номер.");
            }

            Tenant tenant = new Tenant();
            tenant.Id = Guid.NewGuid();
            tenant.FirstName = requestData.FirstName;
            tenant.MiddleName = requestData.MiddleName;
            tenant.LastName = requestData.LastName;

            ApartmentTenant apartmentTenant = new ApartmentTenant();
            apartmentTenant.TenantId = tenant.Id;
            apartmentTenant.ApartmentId = requestData.ApartmentId;
            apartmentTenant.IsOwner = requestData.IsOwner;
            tenant.ApartmentTenants.Add(apartmentTenant);

            tenant = await this._tenantService.CreateTenantAsync(tenant);

            var responseData = new ApartmentTenantDTO();
            responseData.TenantId = apartmentTenant.TenantId;
            responseData.FirstName = tenant.FirstName;
            responseData.MiddleName = tenant.MiddleName;
            responseData.LastName = tenant.LastName;
            responseData.ApartmentId = apartmentTenant.ApartmentId;
            responseData.IsOwner = apartmentTenant.IsOwner;

            return this.Ok(responseData);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateApartmentTenantDataAsync([FromForm] string key, [FromForm] string values)
        {
            var keyObject = new ApartmentTenantDTO();
            JsonConvert.PopulateObject(key, keyObject);

            var tenant = await this._tenantService.GetTenantByIdAsync(keyObject.TenantId);
            if (tenant == null)
            {
                throw new Exception("Неизвестный жилец.");
            }

            var apartmentTenant = tenant.ApartmentTenants.FirstOrDefault(x => x.ApartmentId == keyObject.ApartmentId);
            if (apartmentTenant == null)
            {
                throw new Exception("Квартира не выбрана или указан неверный номер.");
            }

            ApartmentTenantDTO apartmentTenantDto = new ApartmentTenantDTO();
            JsonConvert.PopulateObject(values, tenant);
            JsonConvert.PopulateObject(values, apartmentTenant);
            JsonConvert.PopulateObject(values, apartmentTenantDto);

            if (values.Contains("firstName") || values.Contains("middleName") || values.Contains("lastName"))
            {
                tenant = await this._tenantService.UpdateTenantAsync(tenant);
            }

            if (values.Contains("apartmentId") || values.Contains("isOwner"))
            {
                ApartmentTenant apartmentTenantToUpdate = new ApartmentTenant();
                apartmentTenantToUpdate.ApartmentId = apartmentTenant.ApartmentId;
                apartmentTenantToUpdate.TenantId = apartmentTenant.TenantId;
                apartmentTenantToUpdate.IsOwner = apartmentTenant.IsOwner;

                apartmentTenant.Tenant = null;
                apartmentTenant =
                    await this._tenantService.UpdateApartmentTenantAsync(keyObject.ApartmentId, keyObject.TenantId,
                        apartmentTenantToUpdate);
            }

            var responseData = new ApartmentTenantDTO();
            responseData.TenantId = apartmentTenant.TenantId;
            responseData.FirstName = tenant.FirstName;
            responseData.MiddleName = tenant.MiddleName;
            responseData.LastName = tenant.LastName;
            responseData.ApartmentId = apartmentTenant.ApartmentId;
            responseData.IsOwner = apartmentTenant.IsOwner;

            return this.Ok(responseData);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApartmenttenantAsync([FromForm] string key)
        {
            var keyObject = new ApartmentTenantDTO();
            JsonConvert.PopulateObject(key, keyObject);
            if (keyObject.ApartmentId == Guid.Empty)
            {
                throw new Exception("Квартира не выбрана или указан неверный номер.");
            }

            if (keyObject.TenantId == Guid.Empty)
            {
                throw new Exception("Жилец не выбран.");
            }

            var tenant = await this._tenantService.GetTenantByIdAsync(keyObject.TenantId);
            if (tenant == null)
            {
                throw new Exception("Жилец не выбран.");
            }

            await this._tenantService.DeleteApartmentTenantAsync(keyObject.TenantId, keyObject.ApartmentId);

            return this.Ok();
        }
    }
}