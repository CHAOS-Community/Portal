namespace Chaos.Portal.Index
{
    using System.Collections.Generic;

    using Chaos.Portal.Exceptions;

    using CHAOS.Index;

    /// <summary>
    /// The view manager.
    /// </summary>
    public class ViewManager : IViewManager
    {
        #region Fields

        /// <summary>
        /// The _loaded views.
        /// </summary>
        private readonly IDictionary<string, IView> _loadedViews;

        #endregion
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewManager"/> class.
        /// </summary>
        public ViewManager()
        {
            _loadedViews = new Dictionary<string, IView>();
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Index(object obj)
        {
            Index(new[] {obj});
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">The objects.</param>
        public void Index(IEnumerable<object> obj)
        {

            foreach (var view in _loadedViews.Values)
                view.Index(obj);
        }

        public IEnumerable<IIndexResult> Query(string key, IQuery query)
        {
            if (!_loadedViews.ContainsKey(key)) throw new ViewNotLoadedException(string.Format("No key with name: '{0}' has been loaded", key));

            return _loadedViews[key].Query(query);
        }

        public void AddView(string key, IView view)
        {
            _loadedViews.Add(key, view);
        }

        #endregion
    }

}