using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Module;
using Geckon.Portal.Data;
using Geckon.Portal.Data.Result;
using Geckon.Portal.Data.Result.Standard;

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

        #endregion
        #region Method call

        public IModuleResult InvokeMethod(IMethodQuery methodQuery)
        {
            IModuleResult    modelResult = new ModuleResult( GetType().FullName );
            IMethodSignature method      = RegisteredMethods[ methodQuery.EventType.EventName + ":" + methodQuery.EventType.Type ];

            try
            {
                object result = method.Method.Invoke( this, GetRelevantParameters( method.Parameters, methodQuery ) );
            
                if( result is IResult )
                    modelResult.AddResult( (IResult) result );
                else
                if( result is IEnumerable<IResult> )
                    modelResult.AddResult( (IEnumerable<IResult>) result );
                else
                if( result is Geckon.Index.IPagedResult<IResult> )
                {
                    Geckon.Index.IPagedResult<IResult> pagedResult = (Geckon.Index.IPagedResult<IResult>) result;

                    modelResult.AddResult( pagedResult.Results );
                    modelResult.TotalCount = pagedResult.FoundCount;
                }
                else
                    throw new UnsupportedModuleReturnType( "Only a return type of IResult or IEnumerable<IResult> is supported" );

            }
            catch( System.Exception ex )
            {
                modelResult.AddResult( new Error( ex.InnerException ?? ex ) );
            }

            return modelResult;
        }

        public bool ContainsServiceHook( string extension, string action )
        {
            return  RegisteredMethods.ContainsKey( extension + ":" + action );
        }

        private object[] GetRelevantParameters( IParameter[] parameters, IMethodQuery methodQuery )
        {
            return parameters.Select( parameter => methodQuery.Parameters[ parameter.ParameterName ].Value ).ToArray();
        }

        #endregion

        #endregion
    }
}
