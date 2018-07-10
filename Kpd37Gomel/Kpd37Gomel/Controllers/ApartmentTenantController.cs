using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.IServices;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    [Authorize(Policy = "OnlyApiAdmin")]
    public class ApartmentTenantController : ODataController
    {
        public readonly IApartmentService _apartmentService;

        public ApartmentTenantController(IApartmentService apartmentService)
        {
            this._apartmentService = apartmentService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var apartments = await this._apartmentService.GetApartmentsAsync();

                return this.Ok(apartments.SelectMany(x => x.ApartmentTenants).AsQueryable());
            }
            catch (Exception)
            {
                return this.BadRequest(new { error = new { message = "Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору." } });
            }
        }
    }
}