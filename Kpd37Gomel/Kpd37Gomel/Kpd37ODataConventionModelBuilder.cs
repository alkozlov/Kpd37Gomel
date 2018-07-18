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
            builder.EntitySet<VoteVariant>("VoteVariant");
            builder.EntitySet<ApartmentVoteChoice>("ApartmentVoteChoice");

            builder.EntityType<Vote>()
                .Action("SendVote")
                .Parameter<Guid>("VariantId");

            var getCommonResultsFunction = builder.EntityType<Vote>().Function("GetCommonResults");
            getCommonResultsFunction.ReturnsCollection<VoteChoiseTinyDTO>();

            var getDetailedResultsFunction = builder.EntityType<Vote>().Function("GetDetailedResults");
            getDetailedResultsFunction.ReturnsCollectionFromEntitySet<ApartmentVoteChoice>("ApartmentVoteChoice");

            return builder.GetEdmModel();
        }
    }
}