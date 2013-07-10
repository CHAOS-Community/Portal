namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Collections.Generic;

    public interface IIndexResponse<TReturnType> where TReturnType : IIndexResult, new()
    {
        Header Header { get; }
        FacetResult FacetResult { get;}
        IList<IQueryResultGroup<TReturnType>> QueryResultGroups { get; }
        IQueryResult<TReturnType> QueryResult { get; }
    }
}