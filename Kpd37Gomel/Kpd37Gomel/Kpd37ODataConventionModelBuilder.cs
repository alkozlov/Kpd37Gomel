﻿using Kpd37Gomel.DataAccess.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace Kpd37Gomel
{
    static class Kpd37ODataConventionModelBuilder
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Apartment>("Apartment");
            builder.EntitySet<ApartmentTenant>("ApartmentTenant").EntityType
                .HasKey(x => new {x.ApartmentId, x.TenantId});
            builder.EntitySet<Tenant>("Tenant");
            builder.EntitySet<Vote>("Vote");
            builder.EntitySet<VoteVariant>("VoteVariant");
            builder.EntitySet<VoteChoice>("VoteChoice");

            return builder.GetEdmModel();
        }
    }
}