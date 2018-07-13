using System;
using Kpd37Gomel.DataAccess.Models;
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
            builder.EntitySet<Tenant>("Tenant");
            builder.EntitySet<Vote>("Vote");
            builder.EntitySet<VoteVariant>("VoteVariant");
            builder.EntitySet<VoteChoice>("VoteChoice");

            builder.EntityType<Vote>()
                .Action("SendVote")
                .Parameter<Guid>("VariantId");

            return builder.GetEdmModel();
        }
    }
}