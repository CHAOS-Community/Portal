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
        ViewInfo GetView(string viewName);

        IEnumerable<ViewInfo> LoadedViews { get; }

        void Delete();

        /// <summary>
        /// Sends a delete query to all Views
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

        void AddView(ViewInfo viewInfo, bool force = false);
    }
}