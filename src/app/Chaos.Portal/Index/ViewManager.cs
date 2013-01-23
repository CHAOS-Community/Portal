namespace Chaos.Portal.Index
{
    using System.Collections.Generic;

    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Data.Dto.Standard;
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
        public ViewManager() : this(new Dictionary<string, IView>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewManager"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public ViewManager(IDictionary<string, IView> dictionary)
        {
            _loadedViews = dictionary;
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">The object.</param>
        public IIndexReport Index(object obj)
        {
            return Index(new[] {obj});
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">The objects.</param>
        public IIndexReport Index(IEnumerable<object> obj)
        {
            var report = new IndexReport();
            
            foreach (var view in _loadedViews.Values)
                report.Views.Add(view.Index(obj));

            return report;
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