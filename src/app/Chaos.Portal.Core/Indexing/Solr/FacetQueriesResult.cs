namespace Chaos.Portal.Core.Indexing.Solr
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class FacetQueriesResult : IFacetQueriesResult
    {
        #region Properties

        public string Value { get; set; }
        public IEnumerable<IFacet> Facets { get; set; }

        #endregion
        #region Constructors

        public FacetQueriesResult(XElement element)
        {
            this.Value = element.Attribute("name").Value;
            this.Facets = element.Elements("int").Select(item => new Facet("int", item.Attribute("name").Value, uint.Parse(item.Value))).ToList();
        }

        #endregion
    }
}