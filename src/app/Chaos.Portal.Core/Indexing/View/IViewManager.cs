namespace Chaos.Portal.Core.Indexing.View
{
    using System.Collections.Generic;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Indexing.Solr;

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
        void Index(IEnumerable<object> objects);

        /// <summary>
        /// Retrieves a view by name
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        IView GetView(string viewName);

        /// <summary>
        /// The add view.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="view">
        /// The view.
        /// </param>
        void AddView(IView view);

        IEnumerable<IView> LoadedViews { get; }

        void Delete();
    }
}