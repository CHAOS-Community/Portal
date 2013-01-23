namespace Chaos.Portal.Index
{
    using System.Collections.Generic;

    using CHAOS.Index;

    using Chaos.Portal.Data.Dto;

    /// <summary>
    /// The View interface.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="obj">
        /// The object. 
        /// </param>
        /// <returns>
        /// The <see cref="IViewReport"/>.
        /// </returns>
        IViewReport Index(IEnumerable<object> obj);

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<IIndexResult> Query(IQuery query);
    }
}