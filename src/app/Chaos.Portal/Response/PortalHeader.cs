namespace Chaos.Portal.Response
{
    public class PortalHeader : IPortalHeader
    {
        #region Properties

        public uint Duration { get; private set; }
        public ReturnFormat ReturnFormat { get; set; }

        #endregion
    }
}