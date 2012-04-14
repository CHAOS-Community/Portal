using CHAOS.Portal.Core.Request;

namespace CHAOS.Portal.Core.Standard
{
    public class CallContext : ICallContext
    {
        #region Properties

        public PortalApplication PortalApplication { get; set; }
        public IPortalRequest    PortalRequest { get; set; }

        #endregion
        #region Constructors

        public CallContext( PortalApplication portalApplication, IPortalRequest portalRequest )
        {
            PortalApplication = portalApplication;
            PortalRequest     = portalRequest;
        }

        #endregion
    }
}
