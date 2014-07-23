namespace Chaos.Portal.Core.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cache;
    using Exceptions;

    /// <summary>
    /// Handles loading and convinience methods for invoking all views
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
            ViewInfos = new Dictionary<string, ViewInvoker>();
            Cache = cache;
        }

        #endregion
        #region Properties

        public IEnumerable<ViewInvoker> LoadedViews
        {
            get
            {
                return ViewInfos.Select(item => item.Value);
            }
        }

        public IDictionary<string, ViewInvoker> ViewInfos { get; private set; }

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
        /// <param name="objectsToIndex">The objects.</param>
        public void Index(IEnumerable<object> objectsToIndex)
        {
            var objects = objectsToIndex as List<object> ?? objectsToIndex.ToList();

            Parallel.ForEach(LoadedViews, (viewInfo) => viewInfo.Index(objects));
        }

        public ViewInvoker GetView(string viewName)
        {
            if (!ViewInfos.ContainsKey(viewName)) throw new ViewNotLoadedException(string.Format("No key with name: '{0}' has been loaded", viewName));

            var viewInfo = ViewInfos[viewName];

            return viewInfo;
        }

        /// <summary>
        /// Sends a delete all query to all Views, essentially cleaning the entire index
        /// </summary>
        public void Delete()
        {
            foreach (var view in LoadedViews)
            {
                view.Delete();
            }
        }

        /// <summary>
        /// Sends a delete query to all Views
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            foreach (var view in LoadedViews)
            {
                view.Delete(id);
            }
        }

        public void AddView(ViewInvoker viewInvoker, bool force = false)
        {
            if (viewInvoker == null) throw new NullReferenceException("ViewFactory cannot be null");
            if (string.IsNullOrEmpty(viewInvoker.Name)) throw new ArgumentException("ViewName has to have a valid value");

            if (ViewInfos.ContainsKey(viewInvoker.Name)) TryReplaceView(viewInvoker.Name, viewInvoker, force);
            else ViewInfos.Add(viewInvoker.Name, viewInvoker);
        }

        private void TryReplaceView(string name, ViewInvoker viewInvoker, bool force)
        {
            if (force) 
                ViewInfos[name] = viewInvoker;
            else 
                throw new DuplicateViewException("View named " + name +" already exist.");
        }

        #endregion
    }
}