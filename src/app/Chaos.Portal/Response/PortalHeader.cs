using System;
using CHAOS.Serialization;

namespace Chaos.Portal.Response
{
    public class PortalHeader : IPortalHeader
    {
        #region Fields

        private readonly DateTime _time;
        
        #endregion
        #region Properties

        [Serialize]
        public uint Duration
        {
            get { return (uint) DateTime.Now.Subtract(_time).TotalMilliseconds; }
        }
        public ReturnFormat ReturnFormat { get; set; }

        #endregion
        #region Initialization

        public PortalHeader(DateTime time)
        {
            _time = time;
        }

        #endregion
    }
}