using CHAOS.Portal.Core.Standard;

namespace CHAOS.Portal.Core
{
    public class PortalResponse : IPortalResponse
    {
        #region Properties

        public IPortalResult PortalResult{ get; set; }

        #endregion
        #region Constructors

        public PortalResponse()
        {
            PortalResult = new PortalResult();
        }

        #endregion
    }
}
