namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Collections.Generic;

    public interface IFacetsResult
    {
        string Value { get; set; }
        IEnumerable<IFacet> Facets { get; set; }
    }
}