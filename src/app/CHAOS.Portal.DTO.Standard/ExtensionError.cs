using System.Diagnostics;
using CHAOS.Serialization;

namespace CHAOS.Portal.DTO.Standard
{
    [Serialize("Error")]
    public class ExtensionError : Error, IPortalResult
    {
        #region Properties

        public long Duration
        {
            get { return Timestamp.ElapsedMilliseconds; }
        }

        public IModuleResult GetModule(string modulename)
        {
            throw new System.NotImplementedException();
        }

        private Stopwatch Timestamp { get; set; }

        #endregion
        #region Construction

        public ExtensionError(System.Exception exception, Stopwatch timestamp)
            : base(exception)
        {
            Timestamp = timestamp;
            Timestamp.Start();
        }

        #endregion
    }
}
