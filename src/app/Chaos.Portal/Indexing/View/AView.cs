namespace Chaos.Portal.Indexing.View
{
    using System.Collections.Generic;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Indexing.Solr;

    public abstract class AView : IView
    {
        #region Properties

        protected ICache _cache;
        protected IIndex _index;
        protected IPortalApplication _portalApplication;

        private string _name;

        #endregion
        #region Initialization

        protected AView()
        {
            _name = GetType().Name;
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
            _portalApplication = portalApplication;

            return this;
        }

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

            foreach (var obj in objectsToIndex)
            {
                if(!CanMap(obj)) continue;

                var mapped   = Map(obj);
                var didStore = _cache.Store(CreateKey(mapped.UniqueIdentifier.Value), mapped);

                if (didStore) list.Add(mapped);
            }
            
            _index.Index(list);
        }

        protected string CreateKey(string key)
        {
            return string.Format("{0}_{1}", _name, key);
        }

        #endregion
    }
}