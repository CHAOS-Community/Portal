using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Geckon.Portal.Data;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Module
{
    public abstract class AModule : IModule
    {
        #region Properties

        public string Name { get; private set; }
        public IPortalContext PortalContext { get; private set; }
        public IDictionary<string, IMethodSignature> RegisteredMethods { get; protected set; }

        #endregion
        #region Constructor

        protected AModule()
        {
            Name              = GetType().FullName;
            RegisteredMethods = new Dictionary<string, IMethodSignature>();

            CacheMethodSignatures();
        }

        private void CacheMethodSignatures()
        {
            foreach( MethodInfo method in GetType().GetMethods() )
            {
                object[] attributes = method.GetCustomAttributes( typeof( Datatype ), false );

                if( attributes.Length > 0 )
                {
                    Datatype datatype = (Datatype) attributes[0];

                    RegisteredMethods.Add( datatype.Event == null ? method.Name : datatype.Event,
                                           new MethodSignature( datatype, 
                                                                method, 
                                                                method.GetParameters().Select( info => new Parameter( info.Name, info.GetType() ) ).ToArray() ) 
                                          );
                }
            }
        }

        public void Init( IPortalContext portalContext, XDocument config )
        {
            Init( portalContext, config.Root );
        }

        public void Init( IPortalContext portalContext, XElement config )
        {
            PortalContext = portalContext;

            Init( config );
        }

        public abstract void Init( XElement config );

        #endregion
        #region Business Logic

        #region Data

        protected PortalDataContext GetNewPortalDataContext()
        {
            return new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString );
        }

        #endregion
        #region Method call

        public XmlSerialize InvokeMethod( IMethodQuery methodQuery )
        {
            IMethodSignature method = RegisteredMethods[ methodQuery.EventType.EventName ];

            // TODO: Error Handling so nice Exceptions are thrown in case of signature mismatch 
            return ( XmlSerialize ) method.Method.Invoke( this, GetRelevantParameters( method.Parameters, methodQuery ) );
        }

        public bool ContainsMethodSignature( IMethodQuery methodQuery )
        {
            return RegisteredMethods.ContainsKey( methodQuery.EventType.EventName ) && RegisteredMethods[ methodQuery.EventType.EventName ].Datatype.EventType == methodQuery.EventType.Type;
        }

        private object[] GetRelevantParameters( Parameter[] parameters, IMethodQuery methodQuery )
        {
            return parameters.Select( parameter => methodQuery.Parameters[ parameter.ParameterName ].Value ).ToArray();
        }

        #endregion

        #endregion
    }
}
