using System;
using System.Collections.Generic;
using Chaos.Portal.Standard;

namespace Chaos.Portal.Request
{
    public class PortalRequest : IPortalRequest
    {
        #region Properties

        public string                     Extension    { get; protected set; }
        public string                     Action       { get; protected set; }
        public IDictionary<string,string> Parameters   { get; protected set; }
		public IEnumerable<FileStream>    Files        { get; protected set; }
        public ReturnFormat               ReturnFormat { get; private set; }

        #endregion
        #region Constructors

        public PortalRequest( string extension, string action, IDictionary<string,string> parameters, IEnumerable<FileStream> files )
        {
            Extension    = extension;
            Action       = action;
            Parameters   = parameters;
			Files        = files;
            ReturnFormat = Parameters.ContainsKey( "format" ) ? (ReturnFormat) Enum.Parse( typeof( ReturnFormat ), Parameters["format"].ToUpper() ) : ReturnFormat.XML;
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
