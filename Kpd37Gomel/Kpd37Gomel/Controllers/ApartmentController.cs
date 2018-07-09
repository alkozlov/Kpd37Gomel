using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Kpd37Gomel.DataAccess;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DataAccess.Models;
using Kpd37Gomel.DTO;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kpd37Gomel.Controllers
{
    // https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
    // http://qaru.site/questions/248370/using-dtos-with-odata-web-api
    // http://docs.oasis-open.org/odata/odata-json-format/v4.0/os/odata-json-format-v4.0-os.html#_Toc372793091

    [Authorize(Policy = "OnlyApiAdmin")]
    public class ApartmentController : ODataController
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IApartmentService _apartmentService;

        public ApartmentController(IMapper mapper, ApplicationDbContext context, IApartmentService apartmentService)
        {
            this._mapper = mapper;
            this._context = context;
            this._apartmentService = apartmentService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                //var apartments = this._context.Apartments
                //    .Include(i => i.ApartmentTenants).ThenInclude(i => i.Apartment)
                //    .AsQueryable();

                //return this.Ok(apartments.Select(x => this._mapper.Map<ApartmentDTO>(x)));

                var apartments = await this._context.Apartments
                    .Include(i => i.ApartmentTenants).ThenInclude(i => i.Apartment)
                    .ToListAsync();

                return this.Ok(apartments.AsQueryable());
            }
            catch (Exception)
            {
                return this.BadRequest(new { error = new { message = "Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору." } });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Apartment requestApartment)
        {
            try
            {
                var apartments = await this._apartmentService.GetApartmentsAsync();
                if (apartments.Any(x => x.ApartmentNumber == requestApartment.ApartmentNumber))
                {
                    return this.BadRequest(new { error = new { message = "Квартира с таким номером уже существует в базе." } });
                }

                var apartment = this._mapper.Map<Apartment>(requestApartment);
                apartment.Id = Guid.NewGuid();
                var createdApartment = await this._apartmentService.CreateApartmentAsync(apartment);

                return this.Created(createdApartment);
            }
            catch (Exception)
            {
                return this.BadRequest(new { error = new { message = "Произошла непредвиденная ошибка. Пожалуйста обратитесь к администратору." } });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] Guid key, [FromBody] Delta<Apartment> requestApartment)
        {
            var existingApartment = await this._apartmentService.GetApartmentByIdAsync(key);
            if (existingApartment == null)
            {
                return this.BadRequest(new {message = "Квартира не найдена."});
            }

            requestApartment.Patch(existingApartment);
            existingApartment = await this._apartmentService.UpdateApartmentAsync(key, existingApartment);

            return this.Updated(existingApartment);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var existingApartment = await this._apartmentService.GetApartmentByIdAsync(key);
            if (existingApartment == null)
            {
                return this.BadRequest(new { message = "Квартира не найдена." });
            }

            await this._apartmentService.DeleteApartmentAsync(key);

            return this.StatusCode((int) HttpStatusCode.NoContent);
        }
    }
}