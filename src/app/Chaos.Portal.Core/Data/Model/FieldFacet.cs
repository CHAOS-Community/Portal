namespace Chaos.Portal.Core.Data.Model
{
    using System.Collections.Generic;

    using CHAOS.Serialization;

    [Serialize("FieldFacet")]
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