namespace Chaos.Portal.Core.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Core.Cache;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Indexing.Solr;
    using Chaos.Portal.Core.Indexing.Solr.Response;

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

        public virtual IGroupedResult<IResult> GroupedQuery(IQuery query)
        {
            throw new NotImplementedException("Grouping not implemented on this view");
        }

        public virtual FacetResult FacetedQuery(IQuery query)
        {
            var response = Core.Query(query);

            return response.FacetResult;
        }

        public virtual IPagedResult<IResult> Query(IQuery query)
        {
            throw new NotImplementedException("Querying not implemented on this view");
        }

        #endregion
        #region Properties

        public IPortalApplication PortalApplication { get; set; }

        public string Name { get; private set; }

        #endregion
        #region Business Logic

        public abstract IList<IViewData> Index( object objectsToIndex );

        public void Index(IEnumerable<object> objectsToIndex)
        {
            foreach(var viewResults in objectsToIndex.Select(Index).Where(viewResults => viewResults != null))
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

        protected IPagedResult<IResult> Query<TResult>(IQuery query) where TResult : class, IResult
        {
            var response   = Core.Query( query );
            var keys       = response.QueryResult.Results.Select( item => CreateKey( item.Id ) );
            var startIndex = response.QueryResult.StartIndex;
            var foundCount = response.QueryResult.FoundCount;
            var results    = Cache.Get<TResult>(keys);

            return new Data.Model.PagedResult<TResult>(foundCount, startIndex, results);
        }

        protected string CreateKey(string key)
        {
            return string.Format( "{0}_{1}", Name, key.Replace( " ", "" ) );
        }

        #endregion
    }
}