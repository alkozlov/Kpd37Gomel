using System;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DataAccess.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    [Authorize(Policy = "OnlyApiAdmin")]
    public class TenantController : BaseODataController
    {
        private readonly IApartmentService _apartmentService;
        private readonly ITenantService _tenantService;

        public TenantController(IApartmentService apartmentService, ITenantService tenantService)
        {
            this._apartmentService = apartmentService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tenants = await this._tenantService.GetTenantsAsync();

                return this.Ok(tenants);
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }

        [HttpPost]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Tenant requestTenant)
        {
            try
            {
                var apartment = await this._apartmentService.GetApartmentByIdAsync(requestTenant.ApartmentId);
                if (apartment == null)
                {
                    return this.BadRequest("Квартира с указанным номером не найдена.");
                }

                requestTenant.Id = Guid.NewGuid();
                requestTenant = await this._tenantService.CreateTenantAsync(requestTenant);

                return this.Created(requestTenant);
            }
            catch (Exception)
            {
                return this.BadRequest(new { error = new { message = "Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору." } });
            }
        }

        [HttpPatch]
        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] Guid key, [FromBody] Delta<Tenant> delta)
        {
            var tenant = await this._tenantService.GetTenantByIdAsync(key);
            if (tenant == null)
            {
                return this.BadRequest("Владелец квартиры не найден.");
            }

            delta.Patch(tenant);
            var apartment = await this._apartmentService.GetApartmentByIdAsync(tenant.ApartmentId);
            if (apartment == null)
            {
                return this.BadRequest("Квартиар с указанным номером не найдена.");
            }

            tenant = await this._tenantService.UpdateTenantAsync(tenant);

            return this.Updated(tenant);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var tenant = await this._tenantService.GetTenantByIdAsync(key);
            if (tenant == null)
            {
                return this.BadRequest("Владелец квартиры не найден.");
            }

            await this._tenantService.DeleteTenantAsync(key);

            return this.NoContent();
        }
    }
}