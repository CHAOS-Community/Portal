namespace Chaos.Portal.Core.Indexing.View
{
    using System.Collections.Generic;

    using Chaos.Portal.Core.Cache;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Indexing.Solr;
    using Chaos.Portal.Core.Indexing.Solr.Response;

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
        
        IView WithCache(ICache cache);
        IView WithIndex(IIndex index);
        IView WithPortalApplication(IPortalApplication portalApplication);

        string Name { get; }

        
    }
}