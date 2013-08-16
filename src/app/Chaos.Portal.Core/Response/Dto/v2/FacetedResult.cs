namespace Chaos.Portal.Core.Response.Dto.v2
{
    using System.Collections.Generic;
    using System.Linq;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Indexing.Solr.Response;

    public class FacetedResult : IPortalResult
    {
        #region Initialization

        public FacetedResult(FacetResult facetResult)
        {
            if (facetResult.FacetFieldsResult == null) return;

            FieldFacets = facetResult.FacetFieldsResult.Select(item => new FieldFacet(item.Value, item.Facets.Select(facet => new Facet(facet.Value, facet.Count)).ToList())).ToList();
        }

        #endregion
        #region Properties

        [Serialize]
        public IList<FieldFacet> FieldFacets { get; set; }

        #endregion
    }
}