using System;
using System.Collections.Generic;

namespace Geckon.Portal.Core.Standard
{
    public class MethodQuery : IMethodQuery
    {
        #region Properties

        public IEventType EventType { get; protected set; }
        public IDictionary<string, IParameter> Parameters { get; protected set; }

        #endregion
        #region Construction

        public MethodQuery( string eventName, string type, IDictionary<string, IParameter> parameters )
        {
            EventType  = new EventType( type, eventName );
            Parameters = parameters;
        }

        public MethodQuery( string eventName, string type, IEnumerable<IParameter> parameters )
        {
            EventType  = new EventType( type, eventName );
            Parameters = new Dictionary<string, IParameter>();

            foreach( IParameter parameter in parameters )
            {
                Parameters.Add( parameter.ParameterName, parameter );
            }
        }

        #endregion
    }
}
