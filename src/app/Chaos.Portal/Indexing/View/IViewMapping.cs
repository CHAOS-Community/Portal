namespace Chaos.Portal.Indexing.View
{
    using System.Collections.Generic;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Indexing.Solr;

    /// <summary>
    /// The View interface.
    /// </summary>
    public interface IViewMapping
    {
        void Index(IEnumerable<object> objectsToIndex);

        void Delete();

        IEnumerable<IViewData> Query(IQuery query);
        
        IViewMapping WithCache(ICache cache);
        IViewMapping WithIndex(IIndex index);
        IViewMapping WithPortalApplication(IPortalApplication portalApplication);

        string Name { get; }
    }
}