using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DataAccess.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    [Authorize(Policy = "OnlyApiAdmin")]
    public class ApartmentTenantController : BaseODataController
    {
        private readonly IApartmentService _apartmentService;
        private readonly ITenantService _tenantService;

        public ApartmentTenantController(IApartmentService apartmentService, ITenantService tenantService)
        {
            this._apartmentService = apartmentService;
            this._tenantService = tenantService;
        }

        //[HttpGet]
        //[EnableQuery]
        //public async Task<IActionResult> Get()
        //{
        //    try
        //    {
        //        var apartments = await this._apartmentService.GetApartmentsAsync();

        //        return this.Ok(apartments.SelectMany(x => x.ApartmentTenants).AsQueryable());
        //    }
        //    catch (Exception)
        //    {
        //        return this.BadRequest(new { error = new { message = "Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору." } });
        //    }
        //}

        //[HttpPost]
        //[EnableQuery]
        //public async Task<IActionResult> Post([FromBody] ApartmentTenant apartmentTenant)
        //{
        //    try
        //    {
        //        var apartment = await this._apartmentService.GetApartmentByIdAsync(apartmentTenant.ApartmentId);
        //        if (apartment == null)
        //        {
        //            return this.BadRequest("Квартира с указанным номером не найдена.");
        //        }

        //        apartmentTenant.Tenant.Id = Guid.NewGuid();
        //        apartmentTenant = await this._tenantService.CreateApartmentTenantAsync(apartmentTenant);

        //        return this.Created(apartmentTenant);
        //    }
        //    catch (Exception)
        //    {
        //        return this.BadRequest(new { error = new { message = "Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору." } });
        //    }
        //}

        //[HttpPut]
        //[EnableQuery]
        //public async Task<IActionResult> Put([FromODataUri] Guid keyApartmentId, [FromODataUri] Guid keyTenantId, [FromBody] ApartmentTenant requestApartmentTenant)
        //{
        //    var apartmentTenant = await this._tenantService.GetApartmentTenantAsync(keyApartmentId, keyTenantId);
        //    if (apartmentTenant == null)
        //    {
        //        return this.BadRequest("Квартира с таким жильцом не найдена.");
        //    }

        //    var tenant = await this._tenantService.GetTenantByIdAsync(keyTenantId);
        //    if (tenant == null)
        //    {
        //        return this.BadRequest("Жилец не найден.");
        //    }

        //    ApartmentTenant modifiedApartmentTenant = new ApartmentTenant();
        //    modifiedApartmentTenant.TenantId = keyTenantId;
        //    modifiedApartmentTenant.ApartmentId = requestApartmentTenant.ApartmentId == Guid.Empty
        //        ? keyApartmentId
        //        : requestApartmentTenant.ApartmentId;
        //    modifiedApartmentTenant.IsOwner = apartmentTenant.IsOwner;
        //    await this._tenantService.UpdateApartmentTenantAsync(keyApartmentId, keyTenantId, modifiedApartmentTenant);

        //    if (requestApartmentTenant.Tenant != null)
        //    {
        //        Tenant tenantToUpdate = new Tenant();
        //        tenantToUpdate.Id = keyTenantId;
        //        tenantToUpdate.IsAdmin = tenant.IsAdmin;
        //        tenantToUpdate.FirstName = !String.IsNullOrEmpty(requestApartmentTenant.Tenant.FirstName?.Trim())
        //            ? requestApartmentTenant.Tenant.FirstName.Trim()
        //            : tenant.FirstName;
        //        tenantToUpdate.MiddleName = !String.IsNullOrEmpty(requestApartmentTenant.Tenant.MiddleName?.Trim())
        //            ? requestApartmentTenant.Tenant.MiddleName.Trim()
        //            : tenant.MiddleName;
        //        tenantToUpdate.LastName = !String.IsNullOrEmpty(requestApartmentTenant.Tenant.LastName?.Trim())
        //            ? requestApartmentTenant.Tenant.LastName.Trim()
        //            : tenant.LastName;

        //        tenantToUpdate = await this._tenantService.UpdateTenantAsync(tenantToUpdate);
        //    }

        //    var responseData = await this._tenantService.GetApartmentTenantAsync(modifiedApartmentTenant.ApartmentId,
        //        modifiedApartmentTenant.TenantId);

        //    return this.Updated(responseData);
        //}

        //[HttpPatch]
        //[EnableQuery]
        ////public async Task<IActionResult> Patch([FromODataUri] Guid keyApartmentId, [FromODataUri] Guid keyTenantId, Delta<ApartmentTenant> delta)
        //public async Task<IActionResult> Patch([FromODataUri] Guid keyApartmentId, [FromODataUri] Guid keyTenantId, [FromBody] Delta<ApartmentTenant> delta)
        //{
        //    var apartmentTenant = await this._tenantService.GetApartmentTenantAsync(keyApartmentId, keyTenantId);
        //    if (apartmentTenant == null)
        //    {
        //        return this.BadRequest("Квартира с таким жильцом не найдена.");
        //    }

        //    apartmentTenant.Apartment = null;
        //    delta.Patch(apartmentTenant);
        //    //delta.ApplyTo(apartmentTenant);
        //    await this._tenantService.UpdateApartmentTenantAsync(keyApartmentId, keyTenantId, apartmentTenant);

        //    return this.Updated(apartmentTenant);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromODataUri] Guid keyApartmentId, [FromODataUri] Guid keyTenantId)
        //{
        //    var apartmentTenant = await this._tenantService.GetApartmentTenantAsync(keyApartmentId, keyTenantId);
        //    if (apartmentTenant == null)
        //    {
        //        return this.BadRequest("Квартира с таким жильцом не найдена.");
        //    }

        //    await this._tenantService.DeleteApartmentTenantAsync(keyApartmentId, keyTenantId);

        //    return this.NoContent();
        //}
    }
}