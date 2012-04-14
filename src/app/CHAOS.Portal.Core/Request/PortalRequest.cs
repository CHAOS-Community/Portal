namespace CHAOS.Portal.Core.Request
{
    public class PortalRequest : IPortalRequest
    {
        #region Properties

        public string      Extension  { get; protected set; }
        public string      Action     { get; protected set; }
        public Parameter[] Parameters { get; protected set; }

        #endregion
        #region Constructors

        public PortalRequest( string extension, string action, params Parameter[] parameters )
        {
            Extension  = extension;
            Action     = action;
            Parameters = parameters;
        }

        #endregion
    }
}
