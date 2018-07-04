using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    [Route("api/tenants")]
    [ApiController]
    [Authorize]
    public class TenantsController : Controller
    {

    }
}