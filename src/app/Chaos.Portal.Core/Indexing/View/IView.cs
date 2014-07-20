namespace Chaos.Portal.Core.Indexing.View
{
    using System.Collections.Generic;
    using Cache;
    using Data.Model;
    using Solr;
    using Solr.Response;

    public interface IView
    {
        void Delete();

        IGroupedResult<IResult> GroupedQuery(IQuery query);
        FacetResult FacetedQuery(IQuery query);
        IPagedResult<IResult> Query(IQuery query);
        IPagedResult<IResult> Query<TResult>(IQuery query) where TResult : class, IResult;

        IView WithIndex(IIndex index);
        IView WithPortalApplication(IPortalApplication portalApplication);

        string Name { get; }
        IIndex Core { get; set; }

        void Delete(string uniqueIdentifier);
        IEnumerable<IIndexable> GetIndexResults(IEnumerable<object> objectsToIndex);
        string CreateKey(string key);

        void Index(List<object> objectsToIndex, ICacheWriter cacheWriter);
        IView WithCache(ICache cache);
        void Initialize(ICacheWriter cacheWriter);
    }
}