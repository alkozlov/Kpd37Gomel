using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kpd37Gomel.Controllers
{
    [Route("api/v1/menu")]
    [ApiController]
    [Authorize]
    public class NavigationMenuController : Controller
    {
        private readonly ITenantService _tenantService;

        public NavigationMenuController(ITenantService tenantService)
        {
            this._tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNavigationMenuItemsAsync()
        {
            var currentUser = this.HttpContext.User;
            var tenantIdClaim = currentUser.Claims.FirstOrDefault(x => x.Type == "tenant_id");
            Guid tenantId;
            if (tenantIdClaim == null || !Guid.TryParse(tenantIdClaim.Value, out tenantId))
            {
                throw new Exception("Неизвестный пользователь.");
            }

            var tenant = await this._tenantService.GetTenantByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new Exception("Неизвестный пользователь.");
            }

            List<MenuItemDTO> menuItems = new List<MenuItemDTO>();
            menuItems.Add(new MenuItemDTO {Title = "Голосования", Route = "/votes", IconClassName = "how_to_vote" });
            if (tenant.IsAdmin)
            {
                menuItems.Add(new MenuItemDTO{Title = "Квартиры", Route = "/flats", IconClassName = "home" });
                menuItems.Add(new MenuItemDTO{Title = "Жильцы", Route = "/tenants", IconClassName = "people" });
                menuItems.Add(new MenuItemDTO{Title = "Создать голосование", Route = "/vote-create", IconClassName = "note_add" });
                //menuItems.Add(new MenuItemDTO{Title = "Создать опросник", Route = "/questionnaire", IconClassName = "glyphicon-bullhorn" });
            }

            return this.Ok(menuItems);
        }
    }
}