namespace Chaos.Portal.Indexing.View
{
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Indexing.Solr;

    public abstract class AViewMapping : IViewMapping
    {
        #region Properties

        protected ICache _cache;
        protected IIndex _index;
        protected IPortalApplication _portalApplication;

        #endregion
        #region Initialization

        protected AViewMapping()
        {
            Name = GetType().Name;
        }

        public IViewMapping WithCache(ICache cache)
        {
            _cache = cache;

            return this;
        }

        public IViewMapping WithIndex(IIndex index)
        {
            _index = index;

            return this;
        }

        public IViewMapping WithPortalApplication(IPortalApplication portalApplication)
        {
            _portalApplication = portalApplication;

            return this;
        }

        #endregion
        #region Properties

        public string Name { get; private set; }

        #endregion
        #region Abstract methods

        public abstract IEnumerable<IViewData> Query(IQuery query);
        protected abstract bool CanMap(object objectToIndex);
        protected abstract IViewData Map(object objectsToIndex);

        #endregion
        #region Business Logic

        public void Index(IEnumerable<object> objectsToIndex)
        {
            var list = new List<IViewData>();

            foreach (var obj in objectsToIndex.Where(CanMap))
            {
                var mapped   = Map(obj);
                var didStore = _cache.Store(CreateKey(mapped.UniqueIdentifier.Value), mapped);

                if (didStore) list.Add(mapped);
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