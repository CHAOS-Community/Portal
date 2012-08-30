using System.Collections.Generic;
using CHAOS.Portal.Core.Standard;

namespace CHAOS.Portal.Core.Request
{
    public class PortalRequest : IPortalRequest
    {
        #region Properties

        public string                     Extension  { get; protected set; }
        public string                     Action     { get; protected set; }
        public IDictionary<string,string> Parameters { get; protected set; }
		public IEnumerable<FileStream>    Files      { get; protected set; }

        #endregion
        #region Constructors

        public PortalRequest( string extension, string action, IDictionary<string,string> parameters, IEnumerable<FileStream> files )
        {
            Extension  = extension;
            Action     = action;
            Parameters = parameters;
			Files      = files;
        }

		public PortalRequest( string extension, string action, IDictionary<string,string> parameters ) : this( extension, action, parameters, new List<FileStream>() )
        {
        }

        public PortalRequest() : this( null, null, new Dictionary<string, string>(), new List<FileStream>() )
        {
            
        }

        #endregion
    }
}
