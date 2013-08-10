namespace Chaos.Portal.Core.Response.Dto.v2
{
    using CHAOS.Serialization;

    [Serialize("PortalResponse")]
    public class PortalResponse
    {
        #region Properties

        [Serialize]
        public IPortalHeader Header{ get; set; }
        [Serialize]
        public IPortalResult Result { get; set; }
        [Serialize]
        public IPortalError  Error { get; set; }

        #endregion
        #region Initialization

        public PortalResponse(IPortalHeader header, IPortalResult result, IPortalError error)
        {
            Header = header;
            Result = result;
            Error  = error;
        }

        #endregion
    }
}
