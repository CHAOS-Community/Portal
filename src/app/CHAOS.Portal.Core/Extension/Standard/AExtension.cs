using System.Collections.Generic;
using System.Reflection;
using CHAOS.Portal.Exception;

namespace CHAOS.Portal.Core.Extension.Standard
{
    public abstract class AExtension : IExtension
    {
        public virtual void CallAction( ICallContext callContext )
        {
            var method     = GetType().GetMethod( callContext.PortalRequest.Action );
            var parameters = BindParameters( callContext, method.GetParameters() );

            method.Invoke( this, parameters );
        }

        private static object[] BindParameters( ICallContext callContext, ICollection<ParameterInfo> parameters )
        {
            var boundParameters = new object[ parameters.Count ];

            foreach( var parameterInfo in parameters )
            {
                if( !callContext.PortalApplication.Bindings.Bindings.ContainsKey( parameterInfo.ParameterType ) )
                    throw new ParameterBindingMissingException( string.Format( "There is no binding for the type:{0}", parameterInfo.ParameterType.FullName ) );
                
                boundParameters[ parameterInfo.Position ] = callContext.PortalApplication.Bindings.Bindings[ parameterInfo.ParameterType ].Bind( callContext, parameterInfo );
            }

            return boundParameters;
        }
    }
}
