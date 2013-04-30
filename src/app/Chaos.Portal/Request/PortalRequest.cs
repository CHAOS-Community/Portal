using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Chaos.Portal.Request
{
    using Chaos.Portal.Core;

    public class PortalRequest : IPortalRequest
    {
        #region Constructors

        public PortalRequest( string extension, string action, IDictionary<string,string> parameters, IEnumerable<FileStream> files )
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();

            Extension    = extension;
            Action       = action;
            Parameters   = parameters;
			Files        = files;
        }

		public PortalRequest( string extension, string action, IDictionary<string,string> parameters ) : this( extension, action, parameters, new List<FileStream>() )
        {
        }

        public PortalRequest() : this( null, null, new Dictionary<string, string>(), new List<FileStream>() )
        {
            
        }

        #endregion
        #region Properties

        public string                     Extension    { get; protected set; }
        public string                     Action       { get; protected set; }
        public IDictionary<string,string> Parameters   { get; protected set; }
		public IEnumerable<FileStream>    Files        { get; protected set; }
        public Stopwatch                  Stopwatch    { get; private set; }

        public ReturnFormat               ReturnFormat
        { 
            get
            {
                return Parameters.ContainsKey("format") ? (ReturnFormat)Enum.Parse(typeof(ReturnFormat), Parameters["format"].ToUpper()) : ReturnFormat.XML;
            } 
        }

        #endregion
    }
}
