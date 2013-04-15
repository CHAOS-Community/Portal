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
        private readonly IDictionary<string, IView> _loadedViews;
        private readonly ICache                     Cache;

        #endregion
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewManager"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public ViewManager(IDictionary<string, IView> dictionary, ICache cache)
        {
            _loadedViews = dictionary;
            Cache       = cache;
        }

        #endregion
        #region Properties

        public IEnumerable<IView> LoadedViews
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

            foreach(var view in _loadedViews.Values)
                view.Index(objects);
        }

        public IPagedResult<IResult> Query(string viewName, IQuery query)
        {
            if (!_loadedViews.ContainsKey(viewName)) throw new ViewNotLoadedException(string.Format("No key with name: '{0}' has been loaded", viewName));

            return _loadedViews[viewName].Query(query);
        }

        public void Delete()
        {
            foreach(var view in LoadedViews)
            {
                view.Delete();
            }
        }

        public void AddView(IView view)
        {
            if(view == null) throw new NullReferenceException("Cannot load a null view");
            if(string.IsNullOrEmpty(view.Name)) throw new ArgumentException("View.Name cannot be null");
            if(_loadedViews.ContainsKey( view.Name )) throw new ArgumentException( "Key already added", view.Name );

            _loadedViews.Add( view.Name, view );
        }

        #endregion
    }

}