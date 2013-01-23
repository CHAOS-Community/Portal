namespace Chaos.Portal.Data.Dto
{
    using System.Collections.Generic;

    /// <summary>
    /// The IndexReport interface.
    /// </summary>
    public interface IIndexReport
    {
        /// <summary>
        /// Gets the views.
        /// </summary>
        IList<IViewReport> Views { get; }
    }
}