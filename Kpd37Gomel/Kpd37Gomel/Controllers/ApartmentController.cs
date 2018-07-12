using System;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DataAccess.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    [Authorize(Policy = "OnlyApiAdmin")]
    public class ApartmentController : ODataController
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentController(IApartmentService apartmentService)
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

                return this.Ok(apartments);
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }

        [HttpPost]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Apartment requestApartment)
        {
            try
            {
                var apartments = await this._apartmentService.GetApartmentsAsync();
                if (apartments.Any(x => x.ApartmentNumber == requestApartment.ApartmentNumber))
                {
                    return this.BadRequest("Квартира с таким номером уже существует в базе.");
                }

                requestApartment.Id = Guid.NewGuid();
                requestApartment = await this._apartmentService.CreateApartmentAsync(requestApartment);

                return this.Created(requestApartment);
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }

        [HttpPatch]
        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] Guid key, [FromBody] Delta<Apartment> requestApartment)
        {
            try
            {
                var existingApartment = await this._apartmentService.GetApartmentByIdAsync(key);
                if (existingApartment == null)
                {
                    return this.BadRequest("Квартира не найдена.");
                }

                requestApartment.CopyChangedValues(existingApartment);
                existingApartment = await this._apartmentService.UpdateApartmentAsync(key, existingApartment);

                return this.Updated(existingApartment);
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            try
            {
                var existingApartment = await this._apartmentService.GetApartmentByIdAsync(key);
                if (existingApartment == null)
                {
                    return this.BadRequest("Квартира не найдена.");
                }

                await this._apartmentService.DeleteApartmentAsync(key);
                return this.NoContent();
            }
            catch (Exception)
            {
                return this.BadRequest("Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору.");
            }
        }
    }
}