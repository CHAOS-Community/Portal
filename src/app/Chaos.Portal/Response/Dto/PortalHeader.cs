namespace Chaos.Portal.Response.Dto
{
    using System.Diagnostics;
    using System.Text;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Response;

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