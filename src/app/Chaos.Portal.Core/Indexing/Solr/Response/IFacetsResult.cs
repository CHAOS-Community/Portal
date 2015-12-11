namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Collections.Generic;

    public interface IFacetsResult
    {
        string Value { get; set; }
        IList<IFacet> Facets { get; set; }
    }
}