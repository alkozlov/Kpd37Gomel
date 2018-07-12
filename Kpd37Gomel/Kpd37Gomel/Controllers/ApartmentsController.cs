using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    [Route("api/v1/apartments")]
    [ApiController]
    [Authorize(Policy = "OnlyApiAdmin")]
    public class ApartmentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IApartmentService _apartmentService;

        public ApartmentsController(IMapper mapper, IApartmentService apartmentService)
        {
            this._mapper = mapper;
            this._apartmentService = apartmentService;
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
    }
}
