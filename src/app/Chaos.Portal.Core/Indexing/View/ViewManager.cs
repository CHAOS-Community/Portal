namespace Chaos.Portal.Core.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cache;
    using Exceptions;

    /// <summary>
    /// The view manager.
    /// </summary>
    public class ViewManager : IViewManager
    {
        #region Fields

        /// <summary>
        /// The _loaded views.
        /// </summary>
        private readonly IDictionary<string, Func<IView>> _viewFactories = new Dictionary<string, Func<IView>>();
        private readonly ICache Cache;

        #endregion
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewManager"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public ViewManager(ICache cache)
        {
            Cache = cache;
        }

        #endregion
        #region Properties

        public IEnumerable<IView> LoadedViews
        {
            get
            {
                return _viewFactories.Select(item => item.Value.Invoke());
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

            foreach (var view in _viewFactories.Values.Select(fac => fac.Invoke()))
                view.Index(objects);
        }

        public IView GetView(string viewName)
        {
            if (!_viewFactories.ContainsKey(viewName)) throw new ViewNotLoadedException(string.Format("No key with name: '{0}' has been loaded", viewName));

            return _viewFactories[viewName].Invoke();
        }

        /// <summary>
        /// Sends a delete all query to all Views, essentially cleaning the entire index
        /// </summary>
        public void Delete()
        {
            foreach(var view in LoadedViews)
            {
                view.Delete();
            }
        }

        /// <summary>
        /// Sends a delete query to all Views
        /// </summary>
        /// <param name="query"></param>
        public void Delete(string query)
        {
            foreach (var view in LoadedViews)
            {
                view.Delete(query);
            }
        }

        public void AddView(IView view, bool force = false)
        {
            if(view == null) throw new NullReferenceException("Cannot load a null view");
            if(string.IsNullOrEmpty(view.Name)) throw new ArgumentException("View.Name cannot be null");
            if (_viewFactories.ContainsKey(view.Name))
            {
                if(!force) throw new DuplicateViewException( "Key already added: " + view.Name );

                _viewFactories[view.Name] = () => view;
            }
            else
                AddView(view.Name, () => view);
        }

        #endregion

        public void AddView(string name, Func<IView> viewFactory)
        {
            _viewFactories.Add(name, viewFactory);
        }
    }

}