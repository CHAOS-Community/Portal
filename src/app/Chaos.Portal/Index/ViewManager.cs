namespace Chaos.Portal.Index
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// The loaded views.
        /// </summary>
        private readonly IDictionary<string, IView> loadedViews;

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
        /// <param name="dictionary">The dictionary object to use</param>
        public ViewManager(IDictionary<string, IView> dictionary)
        {
            loadedViews = dictionary;
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">The object to index</param>
        /// <returns>The <see><cref>IIndexReport</cref></see>.</returns>
        public IIndexReport Index(object obj)
        {
            return Index(new[] { obj });
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="objs">The objects to index.</param>
        /// <returns>The <see><cref>IIndexReport</cref></see>.</returns>
        public IIndexReport Index(IEnumerable<object> objs)
        {
            var objects     = objs as object[] ?? objs.ToArray();
            var indexReport = new IndexReport();

            foreach (var view in this.loadedViews.Values)
            {
                var viewReport = view.Index(objects);

                if(viewReport != null) indexReport.Views.Add(viewReport);
            }

            return indexReport;
        }

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="query">The query.</param>
        /// <returns>The query results from the view</returns>
        /// <exception cref="ViewNotLoadedException">The exception is thrown if no view has been added with the given key</exception>
        public IEnumerable<IIndexResult> Query(string key, IQuery query)
        {
            if (!this.loadedViews.ContainsKey(key)) 
                throw new ViewNotLoadedException(string.Format("No key with name: '{0}' has been loaded", key));

            return this.loadedViews[key].Query(query);
        }

        /// <summary>
        /// The add view.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="view">The view.</param>
        public void AddView(string key, IView view)
        {
            if(key == null) throw new NullReferenceException("Key cannot be null");
            if(view == null) throw new NullReferenceException("A null view cannot be added");
            if(loadedViews.ContainsKey(key)) return;

            this.loadedViews.Add(key, view);
        }

        #endregion
    }

}