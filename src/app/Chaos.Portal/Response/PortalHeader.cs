using System.Diagnostics;
using CHAOS.Serialization;

namespace Chaos.Portal.Response
{
    public class PortalHeader : IPortalHeader
    {
        #region Fields

        private readonly Stopwatch _startTime;
        
        #endregion
        #region Properties

        [Serialize]
        public double Duration
        {
            get { return _startTime.Elapsed.TotalMilliseconds; }
        }
        public ReturnFormat ReturnFormat { get; set; }

        #endregion
        #region Initialization

        public PortalHeader(Stopwatch startTime)
        {
            _startTime = startTime;
        }

        #endregion
    }
}