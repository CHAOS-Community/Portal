using CHAOS.Portal.Core;
using CHAOS.Portal.DTO;
using CHAOS.Portal.DTO.Standard;

namespace CHAOS.Portal.Test
{
    public class MockPortalResponse : IPortalResponse
    {
        #region Properties

        public IPortalResult PortalResult { get; set; }

        #endregion
        #region Constructors

        public MockPortalResponse()
        {
            PortalResult = new PortalResult();
        }

        #endregion
    }
}
