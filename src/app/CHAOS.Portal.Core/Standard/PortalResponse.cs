using CHAOS.Portal.DTO;
using CHAOS.Portal.DTO.Standard;

namespace CHAOS.Portal.Core.Standard
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
