namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class FacetQueriesResult : IFacetQueriesResult
    {
        #region Properties

        public string Value { get; set; }
        public IList<IFacet> Facets { get; set; }

        #endregion
        #region Constructors

        public FacetQueriesResult(XElement element)
        {
            Value = element.Attribute("name").Value;
            Facets = element.Elements("int").Select(item => (IFacet) new Facet("int", item.Attribute("name").Value, uint.Parse(item.Value))).ToList();
        }

        #endregion
    }
}