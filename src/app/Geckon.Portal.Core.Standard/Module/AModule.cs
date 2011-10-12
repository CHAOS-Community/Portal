using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Module;
using Geckon.Portal.Data;
using Geckon.Portal.Data.Result;

namespace Geckon.Portal.Core.Standard.Module
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

                    RegisteredMethods.Add( datatype.ToString(),
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

        public IEnumerable<IResult> InvokeMethod(IMethodQuery methodQuery)
        {
            IMethodSignature method = RegisteredMethods[ methodQuery.EventType.EventName + ":" + methodQuery.EventType.Type ];

            try
            {
                object result = method.Method.Invoke( this, GetRelevantParameters( method.Parameters, methodQuery ) );
            
                if( result is IResult )
                    return ToList( (IResult) result );

                if( result is IEnumerable<IResult> )
                    return (IEnumerable<IResult>) result;
            }
            catch( System.Exception ex )
            {
                
                return ToList( new Error( ex.InnerException ?? ex ) );
            }

            throw new UnsupportedModuleReturnType( "Only a return type of IResult or IEnumerable<IResult> is supported" );
        }

        private IList<IResult> ToList( IResult result )
        {
            IList<IResult> list = new List<IResult>();

            list.Add( result );

            return list;
        }

        public bool ContainsServiceHook( string extension, string action )
        {
            return  RegisteredMethods.ContainsKey( extension + ":" + action );
        }

        private object[] GetRelevantParameters( Parameter[] parameters, IMethodQuery methodQuery )
        {
            return parameters.Select( parameter => methodQuery.Parameters[ parameter.ParameterName ].Value ).ToArray();
        }

        #endregion

        #endregion
    }
}
