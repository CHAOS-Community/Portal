namespace Chaos.Portal.Indexing.View
{
    using System.Collections.Generic;

    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Indexing.Solr;

    using GuidResult = Chaos.Portal.Indexing.Solr.GuidResult;

    /// <summary>
    /// The View interface.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">
        /// The object. 
        /// </param>
        /// <returns>
        /// The <see cref="IViewReport"/>.
        /// </returns>
        IViewReport Index(IEnumerable<object> obj);

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IIndexResponse<GuidResult> Query(IQuery query);
    }
}