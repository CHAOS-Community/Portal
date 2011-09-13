using System.Diagnostics;
using Geckon.Portal.Data.Result;
using Geckon.Serialization;

namespace Geckon.Portal.Core.Standard.Extension
{
    [Serialize("Error")]
    public class ExtensionError : Error, IPortalResult
    {
        #region Properties

        public long Duration
        {
            get { return Timestamp.ElapsedMilliseconds; }
        }

        private Stopwatch Timestamp { get; set; }

        #endregion
        #region Construction

        public ExtensionError( System.Exception exception, Stopwatch timestamp ) : base( exception )
        {
            Timestamp = timestamp;
            Timestamp.Start();
        }

        #endregion
    }
}
