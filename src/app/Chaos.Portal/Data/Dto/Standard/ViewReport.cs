namespace Chaos.Portal.Data.Dto.Standard
{
    /// <summary>
    /// The view report.
    /// </summary>
    public class ViewReport : IViewReport
    {
        #region Initialization

        public ViewReport()
        {
            NumberOfIndexedDocuments = 1;
        }

        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the number of indexed documents.
        /// </summary>
        public uint NumberOfIndexedDocuments { get; set; }

        #endregion
    }
}