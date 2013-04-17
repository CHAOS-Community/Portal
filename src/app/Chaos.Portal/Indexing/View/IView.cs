namespace Chaos.Portal.Indexing.View
{
    using System.Collections.Generic;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Indexing.Solr;

    /// <summary>
    /// The View interface.
    /// </summary>
    public interface IView
    {
        void Index( IEnumerable<object> objectsToIndex );

        void Delete();

        IPagedResult<IResult> Query(IQuery query);
        
        IView WithCache(ICache cache);
        IView WithIndex(IIndex index);
        IView WithPortalApplication(IPortalApplication portalApplication);

        string Name { get; }
    }
}