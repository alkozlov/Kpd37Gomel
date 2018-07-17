using System;
using System.Collections.Generic;
using Kpd37Gomel.DataAccess.Models;
using Kpd37Gomel.DTO;
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
            //builder.EntitySet<VoteVariant>("VoteVariant");
            //builder.EntitySet<ApartmentVoteChoice>("ApartmentVoteChoice");

            builder.EntityType<Vote>()
                .Action("SendVote")
                .Parameter<Guid>("VariantId");

            builder.EntityType<Vote>()
                .Function("GetCommonResult")
                .ReturnsCollection<VoteChoiseTinyDTO>();

            return builder.GetEdmModel();
        }
    }
}