namespace Chaos.Portal.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Exceptions;

    using IQuery = Chaos.Portal.Indexing.Solr.IQuery;

    /// <summary>
    /// The view manager.
    /// </summary>
    public class ViewManager : IViewManager
    {
        #region Fields

        /// <summary>
        /// The _loaded views.
        /// </summary>
        private readonly IDictionary<string, IViewMapping> _loadedViews;
        private readonly ICache                     _cache;

        #endregion
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewManager"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public ViewManager(IDictionary<string, IViewMapping> dictionary, ICache cache)
        {
            _loadedViews = dictionary;
            _cache       = cache;
        }

        #endregion
        #region Properties

        public IEnumerable<IViewMapping> LoadedViews
        {
            get
            {
                return _loadedViews.Select(item => item.Value);
            }
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The IndexReport of the index process</returns>
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
            var objects = obj as List<object> ?? obj.ToList();

            foreach (var view in _loadedViews.Values)
                view.Index(objects);
        }

        public IEnumerable<IResult> Query(string key, IQuery query)
        {
            if (!_loadedViews.ContainsKey(key)) throw new ViewNotLoadedException(string.Format("No key with name: '{0}' has been loaded", key));

            return _loadedViews[key].Query(query);
        }

        public void AddView(string key, IViewMapping view)
        {
            if(key == null) throw new NullReferenceException("Cannot load a view with a null key");
            if(view == null) throw new NullReferenceException("Cannot load a null view");
            if(_loadedViews.ContainsKey(key)) throw new ArgumentException("Key already added", key);

            _loadedViews.Add(key, view);
        }

        #endregion
    }

}