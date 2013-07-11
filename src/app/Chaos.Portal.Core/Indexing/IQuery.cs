namespace Chaos.Portal.Core.Indexing
{
    using Chaos.Portal.Core.Indexing.Solr.Request;

    public interface IQuery
    {
        string Query { get; set; }
        string Sort { get; set; }
        string Facet { get; set; }
        uint PageIndex { get; set; }
        uint PageSize { get; set; }
        IQueryGroupSettings Group { get; set; }

        /// <summary>
        /// This method will initialize the query. Reference the implementation documentation for information on Syntax and limitations
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sort"></param>
        void Init(string query, string facet, string sort, uint pageIndex, uint pageSize);

    }
}
