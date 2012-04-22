using System.Collections.Generic;

namespace CHAOS.Portal.Core.Request
{
    public class PortalRequest : IPortalRequest
    {
        #region Properties

        public string                     Extension  { get; protected set; }
        public string                     Action     { get; protected set; }
        public IDictionary<string,string> Parameters { get; protected set; }

        #endregion
        #region Constructors

        public PortalRequest( string extension, string action, IDictionary<string,string> parameters )
        {
            Extension  = extension;
            Action     = action;
            Parameters = parameters;
        }

        public PortalRequest() : this( null, null, new Dictionary<string, string>() )
        {
            
        }

        #endregion
    }
}
