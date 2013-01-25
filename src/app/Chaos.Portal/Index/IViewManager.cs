namespace Chaos.Portal.Index
{
    using System.Collections.Generic;

    using Chaos.Portal.Data.Dto;

    using CHAOS.Index;

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
        IIndexReport Index(object obj);

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
        IIndexReport Index(IEnumerable<object> obj);

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<IResult> Query(string key, IQuery query);

        /// <summary>
        /// The add view.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="view">
        /// The view.
        /// </param>
        void AddView(string key, IView view);
    }
}