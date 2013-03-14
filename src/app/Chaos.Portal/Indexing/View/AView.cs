namespace Chaos.Portal.Indexing.View
{
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Indexing.Solr;

    public abstract class AView : IView
    {
        #region Fields

        protected ICache _cache;
        protected IIndex _index;

        #endregion
        #region Initialization

        protected AView(string name)
        {
            Name     = name;
            Mappings = new List<IViewMapping>();
        }

        public IView WithCache(ICache cache)
        {
            _cache = cache;

            return this;
        }

        public IView WithIndex(IIndex index)
        {
            _index = index;

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

        protected IList<IViewMapping> Mappings { get; set; }

        public IPortalApplication PortalApplication { get; set; }

        public string Name { get; private set; }

        #endregion
        #region Business Logic

        public void Index(IEnumerable<object> objectsToIndex)
        {
            var list = new List<IViewData>();
            
            foreach(var mapping in Mappings)
            {
                foreach(var obj in objectsToIndex.Where( mapping.CanMap ))
                {
                    var mapped   = mapping.Map( obj );
                    var didStore = _cache.Store(CreateKey(mapped.UniqueIdentifier.Value), mapped);

                    if (didStore) list.Add(mapped);
                }
            }
            
            _index.Index(list);
        }

        public void Delete()
        {
            _index.Delete();
        }

        protected IEnumerable<IViewData> Query<TResult>( IQuery query ) where TResult : class, IViewData
        {
            var response = _index.Query( query );
            var keys     = response.QueryResult.Results.Select( item => CreateKey( item.Id ) );

            return _cache.Get<TResult>( keys );
        }

        protected string CreateKey(string key)
        {
            return string.Format( "{0}_{1}", Name, key.Replace( " ", "" ) );
        }

        #endregion
    }
}