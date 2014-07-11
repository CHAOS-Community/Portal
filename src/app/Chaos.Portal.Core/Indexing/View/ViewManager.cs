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

        private readonly ICache Cache;

        #endregion
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewManager"/> class.
        /// </summary>
        /// <param name="cache"></param>
        public ViewManager(ICache cache)
        {
            ViewFactories = new Dictionary<string, Func<IView>>();
            Cache = cache;
        }

        #endregion
        #region Properties

        public IEnumerable<IView> LoadedViews
        {
            get
            {
                return ViewFactories.Select(item => item.Value.Invoke());
            }
        }

        public IDictionary<string, Func<IView>> ViewFactories { get; private set; }

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

            foreach (var view in ViewFactories.Values.Select(fac => fac.Invoke()))
                view.Index(objects);
        }

        public IView GetView(string viewName)
        {
            if (!ViewFactories.ContainsKey(viewName)) throw new ViewNotLoadedException(string.Format("No key with name: '{0}' has been loaded", viewName));

            return ViewFactories[viewName].Invoke();
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


        [Obsolete("Use the overload AddView(name, viewFactory, forced) instead",true)]
        public void AddView(IView view, bool force = false)
        {
            AddView(view.Name, () => view);
        }

        public void AddView(string name, Func<IView> viewFactory, bool force = false)
        {
            if (viewFactory == null) throw new NullReferenceException("ViewFactory cannot be null");
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("ViewName has to have a valid value");
            
            if (ViewFactories.ContainsKey(name)) TryReplaceView(name, viewFactory, force);
            else ViewFactories.Add(name, viewFactory);
        }

        private void TryReplaceView(string name, Func<IView> viewFactory, bool force)
        {
            if (force) ViewFactories[name] = viewFactory;
            else throw new DuplicateViewException("Key already added: " + name);
        }

        #endregion
    }

}