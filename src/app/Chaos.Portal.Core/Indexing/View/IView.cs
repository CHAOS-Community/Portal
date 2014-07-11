namespace Chaos.Portal.Core.Indexing.View
{
    using System.Collections.Generic;

    using Cache;
    using Data.Model;
    using Solr;
    using Solr.Response;

    /// <summary>
    /// The View interface.
    /// </summary>
    public interface IView
    {
        void Index( IEnumerable<object> objectsToIndex );

        void Delete();

        IGroupedResult<IResult> GroupedQuery(IQuery query);
        FacetResult FacetedQuery(IQuery query);
        IPagedResult<IResult> Query(IQuery query);
        IPagedResult<IResult> Query<TResult>(IQuery query) where TResult : class, IResult;

        IView WithCache(ICache cache);
        IView WithIndex(IIndex index);
        IView WithPortalApplication(IPortalApplication portalApplication);

        string Name { get; }

        void Delete(string uniqueIdentifier);
    }
}