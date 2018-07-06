using System;
using System.Text;
using AutoMapper;
using Kpd37Gomel.DataAccess;
using Kpd37Gomel.DataAccess.IServices;
using Kpd37Gomel.DataAccess.IServices.Implementation;
using Kpd37Gomel.DataAccess.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Kpd37Gomel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = this.Configuration["Jwt:Issuer"],
                        ValidAudience = this.Configuration["Jwt:Issuer"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:Key"])),
                        RequireExpirationTime = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyApiAdmin", policy => { policy.RequireClaim("api_admin"); });
            });

            services.AddOData();
            services.AddTransient<Kpd37GomelModelBuilder>();

            services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connection = this.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            #region IoC Config

            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<IVoteService, VoteService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Kpd37GomelModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes
                    .Select()
                    .Expand()
                    .Filter()
                    .OrderBy(QueryOptionSetting.Allowed)
                    .MaxTop(null)
                    .Count();
                routes.MapODataServiceRoute("OData", "odata", modelBuilder.GetEdmModel(app.ApplicationServices));

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
