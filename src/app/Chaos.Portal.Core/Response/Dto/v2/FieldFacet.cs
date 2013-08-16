namespace Chaos.Portal.Core.Response.Dto.v2
{
    using System.Collections.Generic;

    using CHAOS.Serialization;

    public class FieldFacet
    {
        #region Initialization

        public FieldFacet(string value, IList<Facet> facets)
        {
            Value  = value;
            Facets = facets;
        }

        #endregion
        #region Properties

        [Serialize]
        public string Value { get; set; }

        [Serialize]
        public IList<Facet> Facets { get; set; }

        #endregion
    }
}