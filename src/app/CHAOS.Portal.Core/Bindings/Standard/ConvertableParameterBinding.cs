using System;
using System.Reflection;
using CHAOS.Extensions;
using CHAOS.Portal.Exception;

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

            throw new ParameterBindingMissingException(string.Format("The parameter ({0}) is missing, and the type isnt nullable",parameterInfo.Name));
        }
    }
}
