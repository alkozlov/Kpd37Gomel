using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DataAccess.Models;
using Kpd37Gomel.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kpd37Gomel.Controllers
{
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITenantService _tenantService;

        public AuthenticationController(IConfiguration configuration, ITenantService tenantService)
        {
            this._configuration = configuration;
            this._tenantService = tenantService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogInAsync([FromBody] LoginModelDTO loginModel)
        {
            var tenants = await this._tenantService.GetTenantsAsync();
            var tenant = tenants.FirstOrDefault(x =>
                String.Compare(x.FirstName, loginModel.FirstName, StringComparison.OrdinalIgnoreCase) == 0 &&
                String.Compare(x.MiddleName, loginModel.MiddleName, StringComparison.OrdinalIgnoreCase) == 0 &&
                String.Compare(x.LastName, loginModel.LastName, StringComparison.OrdinalIgnoreCase) == 0);

            if (tenant == null)
            {
                return this.Unauthorized();
            }

            var tokenString = this.CreateJwtToken(tenant);
            return this.Ok(new
            {
                tenantTiny = new
                {
                    IsAdmin = tenant.IsAdmin,
                    FullName = String.Format("{0} {1}.{2}.", tenant.LastName, tenant.FirstName[0], tenant.MiddleName[0]),
                    ApartmentNumber = tenant.ApartmentTenants.FirstOrDefault()?.Apartment.ApartmentNumber ?? String.Empty
                },
                token = tokenString
            });
        }

        private string CreateJwtToken(Tenant tenant)
        {
            // Custom claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("apartment_id", tenant.ApartmentTenants.FirstOrDefault()?.ApartmentId.ToString()),
                new Claim("tenant_id", tenant.Id.ToString())
            };

            if (tenant.IsAdmin)
            {
                claims.Add(new Claim("api_admin", tenant.Id.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                this._configuration["Jwt:Issuer"],
                this._configuration["Jwt:Issuer"],
                claims,
                //expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}