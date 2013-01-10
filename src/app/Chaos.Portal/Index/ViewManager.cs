using System.Collections.Generic;
using CHAOS.Index;
using Chaos.Portal.Exceptions;

namespace Chaos.Portal.Index
{
    public class ViewManager : IViewManager
    {
        #region Fields

        private readonly IDictionary<string, IView> _loadedViews;

        #endregion
        #region Initialization

        public ViewManager()
        {
            _loadedViews = new Dictionary<string, IView>();
        }

        #endregion
        #region Business Logic

        public void Index(object obj)
        {
            Index(new[] {obj});
        }

        public void Index(IEnumerable<object> obj)
        {
            foreach (var view in _loadedViews.Values)
                view.Index(obj);
        }

        public IEnumerable<IIndexResult> Query(string key, IQuery query)
        {
            if (!_loadedViews.ContainsKey(key)) throw new ViewNotLoadedException(string.Format("No key with name: '{0}' has been loaded"));

            return _loadedViews[key].Query(query);
        }

        public void AddView(string key, IView view)
        {
            _loadedViews.Add(key, view);
        }

        #endregion
    }

}