namespace Chaos.Portal.Core.Indexing.Solr
{
    public interface IIndexResponse<TReturnType> where TReturnType : IIndexResult, new()
    {
        Header Header { get; set; }
        QueryResult<TReturnType> QueryResult { get; set; }
        FacetResult FacetResult { get; set; }
    }
}