namespace Chaos.Portal.Indexing.View
{
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Indexing.Solr;

    public abstract class AView : IView
    {
        #region Fields

        protected ICache Cache;
        protected IIndex Core;

        #endregion
        #region Initialization

        protected AView(string name)
        {
            Name = name;
        }

        public IView WithCache(ICache cache)
        {
            Cache = cache;

            return this;
        }

        public IView WithIndex(IIndex index)
        {
            Core = index;

            return this;
        }

        public IView WithPortalApplication(IPortalApplication portalApplication)
        {
            PortalApplication = portalApplication;

            return this;
        }

        #endregion
        #region Abstract

        public abstract IPagedResult<IResult> Query( IQuery query );

        #endregion
        #region Properties

        public IPortalApplication PortalApplication { get; set; }

        public string Name { get; private set; }

        #endregion
        #region Business Logic

        public abstract IList<IViewData> Index( object objectsToIndex );

        public void Index(IEnumerable<object> objectsToIndex)
        {
            foreach(var viewResults in objectsToIndex.Select(Index))
            {
                foreach(var viewResult in viewResults)
                {
                    var key = CreateKey( viewResult.UniqueIdentifier.Value );
                    Cache.Store( key, viewResult );
                }

                Core.Index( viewResults.Select( item => item ) );
            }
        }

        public void Delete()
        {
            Core.Delete();
        }

        protected IEnumerable<IViewData> Query<TResult>( IQuery query ) where TResult : class, IViewData
        {
            var response = Core.Query( query );
            var keys     = response.QueryResult.Results.Select( item => CreateKey( item.Id ) );

            return Cache.Get<TResult>( keys );
        }

        protected string CreateKey(string key)
        {
            return string.Format( "{0}_{1}", Name, key.Replace( " ", "" ) );
        }

        #endregion
    }
}