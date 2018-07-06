using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess;
using Kpd37Gomel.DataAccess.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    //public class ApartmentController : ODataController
    public class ApartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartmentController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Apartment> Get()
        {
            return this._context.Apartments.AsQueryable();
        }
    }
}