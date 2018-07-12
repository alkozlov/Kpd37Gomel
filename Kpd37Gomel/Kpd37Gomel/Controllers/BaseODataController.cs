using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;

namespace Kpd37Gomel.Controllers
{
    public abstract class BaseODataController : ODataController
    {
        protected BadRequestObjectResult BadRequest(string errorMessage)
        {
            return this.BadRequest(new ODataError { Message = errorMessage });
        }
    }
}