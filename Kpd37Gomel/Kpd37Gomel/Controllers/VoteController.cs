using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    public class VoteController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public VoteController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return this.Ok(this._dbContext.Votes.AsQueryable());
        }
    }
}