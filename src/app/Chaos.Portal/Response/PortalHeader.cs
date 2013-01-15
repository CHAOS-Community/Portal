using System.Diagnostics;
using System.Text;
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

        public string Callback { get; set; }
        public Encoding Encoding { get; set; }

        public ReturnFormat ReturnFormat { get; set; }

        #endregion
        #region Initialization

        public PortalHeader(Stopwatch startTime, Encoding encoding)
        {
            _startTime = startTime;
            Encoding   = encoding;
        }

        #endregion
    }
}