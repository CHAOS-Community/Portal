namespace Chaos.Portal.Indexing.View
{
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Indexing.Solr;

    public abstract class AView : IView
    {
        #region Properties

        protected ICache _cache;
        protected IIndex _index;

        private string _name;

        #endregion
        #region Initialization

        protected AView()
        {
            _name = GetType().Name;
        }

        #endregion
        #region Implementation of IView

        public abstract IEnumerable<IViewData> Query(IQuery query);
        protected abstract bool CanMap(object objectToIndex);
        protected abstract IViewData Map(object objectsToIndex);
        
        public void Index(IEnumerable<object> objectsToIndex)
        {
            var list = objectsToIndex.Where(CanMap).Select(Map).ToList();

            foreach (var obj in objectsToIndex)
            {
                if(!CanMap(obj)) continue;

                var mapped   = Map(obj);
                var didStore = _cache.Store(_name + "_" + mapped.UniqueIdentifier.Value, mapped);

                if (didStore) list.Add(mapped);
            }
            
            _index.Index(list);
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

        #endregion
    }
}