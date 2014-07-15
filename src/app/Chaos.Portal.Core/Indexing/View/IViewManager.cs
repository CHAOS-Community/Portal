namespace Chaos.Portal.Core.Indexing.View
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The ViewManager interface.
    /// </summary>
    public interface IViewManager
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// The <see>
        ///       <cref>IIndexReport</cref>
        ///     </see> .
        /// </returns>
        void Index(object obj);

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// The <see>
        ///       <cref>IIndexReport</cref>
        ///     </see> .
        /// </returns>
        void Index(IEnumerable<object> objectsToIndex);

        /// <summary>
        /// Retrieves a view by name
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        IView GetView(string viewName);

        /// <summary>
        /// The add view.
        /// </summary>
        /// <param name="view">
        ///     The view.
        /// </param>
        /// <param name="force"></param>
        /// <param name="key">
        /// The key.
        /// </param>
        void AddView(IView view, bool force = false);

        IEnumerable<IView> LoadedViews { get; }

        void Delete();

        /// <summary>
        /// Sends a delete query to all Views
        /// </summary>
        /// <param name="query"></param>
        void Delete(string query);

        void AddView(string name, Func<IView> viewFactory, bool force = false);
    }
}