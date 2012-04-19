using System;
using System.Reflection;
using CHAOS.Portal.Exception;
using Geckon.Common.Extensions;

namespace CHAOS.Portal.Core.Bindings.Standard
{
    public class ConvertableParameterBinding<T> : IParameterBinding where T : IConvertible
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.PortalRequest.Parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( callContext.PortalRequest.Parameters[ parameterInfo.Name ] ) )
                return Convert.ChangeType( callContext.PortalRequest.Parameters[ parameterInfo.Name ], typeof(T) );
            
            if( parameterInfo.ParameterType.IsNullable() )
                return null;

            throw new ParameterBindingMissingException("The parameter is missing, and the type isnt nullable");
        }
    }
}
