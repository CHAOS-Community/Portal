﻿using System.Collections.Generic;
using System.Reflection;
using CHAOS.Index;
using CHAOS.Portal.DTO;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Exception;

namespace CHAOS.Portal.Core.Module
{
    public abstract class AModule : IModule
    {
        #region Properties



        #endregion
        #region Constructors

        public abstract void Initialize( string configuration );
        
        #endregion
        #region Business Logic

        public virtual void CallAction( ICallContext callContext )
        {
            // REVIEW: Reflection is slow, cache methods for performance
            // Call Method(s) matching the Requested Extension and Action
            foreach( MethodInfo method in GetType().GetMethods() )
            {
                    var attributes  = GetType().GetCustomAttributes( typeof(ModuleAttribute), true );
                    var moduleName  = attributes.Length == 0 ? GetType().FullName : ( (ModuleAttribute) attributes[0] ).ModuleConfigName;
                    var modelResult = callContext.PortalResponse.PortalResult.GetModule( moduleName );

                try
                {
                    foreach( Datatype datatypeAttribute in method.GetCustomAttributes( typeof( Datatype ), true ) )
                    {
                        if( datatypeAttribute.ExtensionName == callContext.PortalRequest.Extension && 
                            datatypeAttribute.ActionName    == callContext.PortalRequest.Action )
                        {

                            var parameters  = BindParameters( callContext, method.GetParameters() );
                            var result      = method.Invoke( this, parameters );

                            // Save result
                            if( result is IResult )
                                modelResult.AddResult( (IResult) result );
                            else
                            if( result is IEnumerable<IResult> )
                                modelResult.AddResult( (IEnumerable<IResult>) result );
                            else
                            if( result is IPagedResult<IResult> )
                            {
                                var pagedResult = (IPagedResult<IResult>) result;

                                modelResult.AddResult( pagedResult.Results );
                                modelResult.TotalCount = pagedResult.FoundCount;
                            }
                            else
                                throw new UnsupportedModuleReturnTypeException( "Only a return type of IResult, IEnumerable<IResult> or PagedResult<IResult> is supported" );
                        }
                    }
                }
                catch( TargetInvocationException e )
                {
                    modelResult.Results.Clear();
                    modelResult.AddResult( new Error( e.InnerException ) );
                }
                catch( System.Exception e )
                {
                    modelResult.Results.Clear();
                    modelResult.AddResult( new Error( e ) );
                }
            }
        }

        private static object[] BindParameters( ICallContext callContext, ICollection<ParameterInfo> parameters )
        {
            var boundParameters = new object[ parameters.Count ];

            foreach( var parameterInfo in parameters )
            {
                if( !callContext.PortalApplication.Bindings.ContainsKey( parameterInfo.ParameterType ) )
                    throw new ParameterBindingMissingException( string.Format( "There is no binding for the type:{0}", parameterInfo.ParameterType.FullName ) );
                
                boundParameters[ parameterInfo.Position ] = callContext.PortalApplication.Bindings[ parameterInfo.ParameterType ].Bind( callContext, parameterInfo );
            }

            return boundParameters;
        }

        #endregion
    }
}
