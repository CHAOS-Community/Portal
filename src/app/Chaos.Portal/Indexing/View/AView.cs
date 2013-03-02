namespace Chaos.Portal.Indexing.View
{
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Indexing.Solr;

    public abstract class AView : IView
    {
        #region Fields

        protected ICache _cache;
        protected IIndex _index;
        protected IList<IViewMapping> _mappings; 

        #endregion
        #region Initialization

        public AView()
        {
            _mappings = new List<IViewMapping>();
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

        public abstract IEnumerable<IViewData> Query( IQuery query );

        #endregion
        #region Properties

        public IPortalApplication PortalApplication { get; protected set; }

        public string Name { get; private set; }

        #endregion
        #region Business Logic

        public void Index(IEnumerable<object> objectsToIndex)
        {
            var list = new List<IViewData>();
            
            foreach(var mapping in _mappings)
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

        protected string CreateKey(string key)
        {
            return string.Format("{0}_{1}", Name, key);
        }

        #endregion
    }
}