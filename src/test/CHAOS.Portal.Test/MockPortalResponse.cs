using CHAOS.Portal.Core;
using CHAOS.Portal.DTO;
using CHAOS.Portal.DTO.Standard;

namespace CHAOS.Portal.Test
{
    using System.IO;

    public class MockPortalResponse : IPortalResponse
    {
        #region Properties

        public IPortalResult PortalResult { get; set; }

        public Stream Stream { get; private set; }

        public Attachment Attachment { get; private set; }

        public void WriteToResponse( object result, object module )
	    {
		    
	    }

	    #endregion
        #region Constructors

        public MockPortalResponse()
        {
            PortalResult = new PortalResult();
        }

        #endregion
    }
}
