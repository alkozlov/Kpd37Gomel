using System;
using Kpd37Gomel.DataAccess.Models;
using Kpd37Gomel.DTO;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace Kpd37Gomel
{
    public class Kpd37GomelModelBuilder
    {
        public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);

            builder.EntitySet<Vote>(nameof(Vote))
                .EntityType
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand() // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select().ContainsMany(x => x.Variants)
                .Expand();

            builder.EntitySet<ApartmentTenant>(nameof(ApartmentTenant))
                .EntityType
                .HasKey(x => new {x.ApartmentId, x.TenantId})
                .ContainsRequired(x => x.Tenant);

            //builder.EntitySet<Apartment>(nameof(Apartment))
            //    .EntityType
            //    .Filter() // Allow for the $filter Command
            //    .Count() // Allow for the $count Command
            //    .Expand() // Allow for the $expand Command
            //    .OrderBy() // Allow for the $orderby Command
            //    .Page() // Allow for the $top and $skip Commands
            //    .Select().ContainsMany(x => x.ApartmentTenants)
            //    .Expand();

            builder.EntitySet<Apartment>("Apartment")
                .EntityType
                .HasKey(x => x.Id)
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand() // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select().ContainsMany(x => x.ApartmentTenants)
                .Expand();

            return builder.GetEdmModel();
        }
    }
}