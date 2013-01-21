namespace Chaos.Portal.Data.Dto.Standard
{
    using System.Collections.Generic;

    /// <summary>
    /// The index report.
    /// </summary>
    public class IndexReport : IIndexReport
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexReport"/> class.
        /// </summary>
        public IndexReport()
        {
            Views = new List<IViewReport>();
        }

        #endregion
        #region Properties

        /// <summary>
        /// Gets the views.
        /// </summary>
        public IList<IViewReport> Views { get; private set; }

        #endregion
    }
}
