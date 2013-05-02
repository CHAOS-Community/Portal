namespace Chaos.Portal.Core.Response.Dto
{
    using System.Diagnostics;

    using CHAOS.Serialization;

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

        #endregion
        #region Initialization

        public PortalHeader(Stopwatch startTime)
        {
            _startTime = startTime;
        }

        #endregion
    }
}